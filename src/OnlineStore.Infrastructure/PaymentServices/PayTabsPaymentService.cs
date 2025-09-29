using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.IdentityModel.Protocols;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.Payment;
using OnlineStore.Core.ValueObjects;
using OnlineStore.Web.DTOs;
using System.Configuration;
using System.Configuration.Internal;
using System.Collections.Specialized;
using Microsoft.IdentityModel.Tokens;
using Jose;
using Serilog.Core;
using Serilog;
using Microsoft.Extensions.Configuration;
using OnlineStore.Infrastructure.Options;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Unicode;
using Microsoft.Extensions.Options;

namespace OnlineStore.Infrastructure.PaymentServices;
public class PayTabsPaymentService : IPaytabPaymentService
{
  private readonly IOrderRepo _orderRepo;
  private readonly PayTabsOptions _payTabsOptions;
  public PayTabsPaymentService(IOptions<PayTabsOptions> payTabsOptions ,IConfiguration configuration,IOrderRepo orderRepo ,IPaymentRepo paymentRepo)
  {
    _payTabsOptions = payTabsOptions.Value;
    _orderRepo = orderRepo;
  }

  static private NameValueCollection? PayTabsSettings = System.Configuration.ConfigurationManager.GetSection("PayTabs") as NameValueCollection;
  static private readonly string APIKey = PayTabsSettings?["api_key"] ?? "";
  static private readonly int profile_id = int.Parse(PayTabsSettings?["api_key"] ?? "0");
  static private readonly string tran_class = PayTabsSettings?["tran_class"] ?? "";
  static private readonly string tran_type = PayTabsSettings?["tran_type"] ?? "";
  static private readonly string paypage_lang = PayTabsSettings?["paypage_lang"] ?? "";

  private Dictionary<string, object> RequestPayload = new Dictionary<string, object>()
  {
    { "profile_id", profile_id},
    { "tran_type" , tran_type},
    { "tran_class" , tran_class},
    { "cart_description" , ""},
    { "cart_id" , ""}, // Order id
    { "cart_currency" , ""},
    { "cart_amount" , 0},
    { "hide_shipping" , true},
    { "paypage_lang" , paypage_lang},
    //{ "callback", "https://OnlineStore/PaytabCallback"},
    //{ "return" , "https://localhost:300/PaytabReturnPage" },
  };

  public async Task<string> GenereateTransactionURL(Order order, CustomerDetails payTabsCustomerDetails, string? description = null)
  {
    if (order.TotalAmount == null) throw new InvalidOperationException("There is no information about the payment amount within the order");
    if (APIKey.IsNullOrEmpty()) throw new ConfigurationErrorsException("Api Key is not provided");

    RequestPayload["cart_currency"] = _payTabsOptions.cart_currency;
    RequestPayload["cart_id"] = order.ID.ToString();
    RequestPayload["cart_amount"] = order.TotalAmount;
    RequestPayload.Add("customer_details", payTabsCustomerDetails);
    RequestPayload[""] = "Items are included in categories: " + 
      string.Join(", ", await _orderRepo.ItemsCategories(order.ID));

    string content = JsonSerializer.Serialize(RequestPayload);

    HttpClient httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("authorization", APIKey);

    httpClient.BaseAddress = new Uri("https://secure-iraq.paytabs.com/");

    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("payment/request",
      new StringContent(content));

    JsonElement jsonElement = await httpResponseMessage.Content.ReadFromJsonAsync<JsonElement>();

    string? redirectUrl = jsonElement.GetProperty("redirect_url").GetString();

    if (string.IsNullOrEmpty(redirectUrl))
    {
      Log.Logger.Error("redirect url is empty");
      throw new IntegrityException("redirect_url is empty");
    }

    return redirectUrl;
  }

  public bool ValidateCallBackPayloadSignature(PayTabCallBackReturnResponse payTabCallBackReturnResponse, string RequestSignature)
  {
    using (HMACSHA256 hMACSHA = new HMACSHA256(Encoding.UTF8.GetBytes(_payTabsOptions.api_key)))
    {
      byte[] HashedResults = hMACSHA.ComputeHash(JsonSerializer.SerializeToUtf8Bytes(payTabCallBackReturnResponse));

      if (Convert.ToHexString(HashedResults) == RequestSignature)
        return true;
      else
        return false;
    }

  }
}
