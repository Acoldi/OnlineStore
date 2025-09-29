using System.Text;
using System.Text.Json;
using Azure;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.Payment;

namespace OnlineStore.Infrastructure.PaymentServices;
public class ZainCashPaymentService : IZainCashPaymentService
{
  private readonly string serviceType = "Order";
  private readonly double MSISDN_Test = 9647835077893;
  private readonly string RedirectURL_Test = "HTTP://domain/api/Payment/ZainCachCallback.com";
  private readonly string secret_Test = "$2y$10$hBbAZo2GfSSvyqAyV2SaqOfYewgYpfR1O19gIh4SqyGWdmySZYPuS";
  private readonly string merchantID_Test = "5ffacf6612b5777c6d44266f";
  private readonly string Language = "en"; // or ar...
  private readonly IPaymentRepo _paymentRepo;
  public ZainCashPaymentService(IPaymentRepo paymentRepo)
  {
    _paymentRepo = paymentRepo;
  }

  public async Task<string> GenerateZainCashPaymentURL(Order order)
  {
    if (order.TotalAmount == null)
      throw new Exception("Total amount can't be null");

    return await _generate_zaincash_url(order, order.TotalAmount.Value, false);
  }

  private async Task<string> _generate_zaincash_url(Order order, decimal amount, bool isdollar)
  {
    //Change currency to dollar if required
    int new_amount;
    if (isdollar) { new_amount = (int)(amount * 1300); } else { new_amount = (int)amount; }

    //Setting expiration of token
    var iat = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    var exp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + 60 * 60 * 4;

    //Generate the data array
    IDictionary<string, object> dataarray = new Dictionary<string, object>();
    dataarray.Add("amount", new_amount);
    dataarray.Add("serviceType", serviceType);
    dataarray.Add("msisdn", MSISDN_Test);
    dataarray.Add("orderId", order.ID);
    dataarray.Add("redirectUrl", RedirectURL_Test);
    dataarray.Add("iat", iat);
    dataarray.Add("exp", exp);

    //Generating token
    var token = Jose.JWT.Encode(dataarray, Encoding.ASCII.GetBytes(secret_Test), Jose.JwsAlgorithm.HS256);

    ////Posting token to ZainCash API to generate Transaction ID
    //var httpclient = new HttpClient();/* new System.Net.WebClient();*/
    //var data_to_post = new System.Collections.Specialized.NameValueCollection();
    //data_to_post["token"] = token;
    //data_to_post["merchantId"] = merchantID_Test;
    //data_to_post["lang"] = Language;

    var data_to_post = new Dictionary<string, string>()
    {
      { "token", token},
      { "merchantId", merchantID_Test},
      { "lang", Language}
    };

    var formUrlEncodedContent = new FormUrlEncodedContent(data_to_post);

    var httpClient = new HttpClient();
    var response = await httpClient.PostAsync
      ("https://api.zaincash.iq/transaction/init", formUrlEncodedContent);

    //Parse JSON response to Object
    var responsee = await response.Content.ReadAsStringAsync();

    var jsona = JsonSerializer.Deserialize<JsonElement>(responsee);

    var transactionID = jsona.GetProperty("id").GetString()!;

    if (transactionID == null)
      throw new Exception("missing transaction id");

    // Createa a payment record
    await _paymentRepo.CreateAsync(new Core.Entities.Payment()
    {
      Amount = amount,
      CreatedAt = DateTime.UtcNow,
      Method = enPaymentMethod.ZainCash,
      OrderID = order.ID,
      Status = enPaymentStatus.Pending,
      TransactionID = transactionID,
      UpdatedAt = DateTime.UtcNow,
    }, null);

    //Return final URL
    return "https://api.zaincash.iq/transaction/pay?id=" + transactionID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="token"></param>
  /// <returns>a dictionary containing the status and a msg error if any</returns>
  public async Task<Dictionary<string, string>> GetZaincashCallBackResults(string token)
  {
    //Convert token to json, then to object
    var jsona_res = JsonSerializer.Deserialize<JsonElement>(Jose.JWT.Decode(token, Encoding.ASCII.GetBytes(secret_Test)));

    //Generating response array
    var status = jsona_res.GetProperty("status").GetString()!;
    var final = new Dictionary<string, string>()
    {
      { "status", status },
      { "id", jsona_res.GetProperty("id").GetString()! },
    };

    if (status == "failed")
    { final.Add("msg", jsona_res.GetProperty("msg").GetString()!); }

    await _UpdatePaymentStatus(status, jsona_res.GetProperty("id").GetString()!);

    return final;
  }

  private async Task<enPaymentStatus> _UpdatePaymentStatus(string status, string transactionID)
  {
    var enPaymentStatus = new enPaymentStatus();

    if (status != "pending")
    {
      // Update payment status record
      Payment payment = await _paymentRepo.GetByTransactionID(transactionID);

      switch (status)
      {
        case "success":
          payment.Status = enPaymentStatus.Completed;
          enPaymentStatus = enPaymentStatus.Completed;
          break;
        case "completed":
          payment.Status = enPaymentStatus.Completed;
          enPaymentStatus = enPaymentStatus.Completed;
          break;
        case "failed":
          payment.Status = enPaymentStatus.Failed;
          enPaymentStatus = enPaymentStatus.Failed;
          break;
        default:
          break;
      }

      await _paymentRepo.UpdateAsync(payment);

      return enPaymentStatus;
    }
    return enPaymentStatus.Pending;
  }

  public async Task<enPaymentStatus> CheckZainCashTransactionStatus(string transactionID)
  {
    var httpClient = new HttpClient();

    var data = new Dictionary<string, object>()
    {
      { "id", transactionID},
      { "msisdn", MSISDN_Test},
      { "iat", DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds},
      { "exp", DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + 60 * 60 * 4},
    };

    var token = Jose.JWT.Encode(data, Encoding.ASCII.GetBytes(secret_Test), Jose.JwsAlgorithm.HS256);

    var keyValuePairs = new Dictionary<string, string>()
    {
      { "token" , token},
      { "merchantId" , merchantID_Test},
    };

    var content = new FormUrlEncodedContent(keyValuePairs);

    var response = await httpClient.PostAsync
      ("https://test.zaincash.iq/transaction/get", content);

    var responsee = await response.Content.ReadAsStringAsync();

    var jsona = JsonSerializer.Deserialize<JsonElement>(responsee);

    return await _UpdatePaymentStatus(jsona.GetProperty("status").GetString()!, jsona.GetProperty("id").GetString()!);
  }
}
