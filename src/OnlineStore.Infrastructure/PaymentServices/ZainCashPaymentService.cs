using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.DTOs.PaymentDtos;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.PaymentServices;
using OnlineStore.Infrastructure.DTOs;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.Infrastructure.Options;
using Serilog;


namespace OnlineStore.Infrastructure.PaymentServices;
public class ZainCashPaymentService : IPaymentService
{
  private const string InitialzePaymentUrl = "https://test.zaincash.iq/transaction/init";
  private const string PaymentUrl = "https://test.zaincash.iq/transaction/pay?id=";
  private readonly IPaymentRepo _paymentRepo;
  private readonly ZainCashOptions _zainCashOptions;
  private readonly HttpClient _httpClient;
  private readonly IOrderRepo _orderRepo;
  public ZainCashPaymentService(IPaymentRepo paymentRepo, ZainCashOptions zainCashOptions,
    HttpClient httpClient, IOrderRepo orderRepo)
  {
    _paymentRepo = paymentRepo;
    _zainCashOptions = zainCashOptions;
    _httpClient = httpClient;
    _orderRepo = orderRepo;
  }

  public async Task<string> GenereateTransactionURL(Order order, CustomerDetailsDto? customerDetailsDto)
  {
    ZainCashPaymentRequestDto zainCashPaymentRequestDto = new ZainCashPaymentRequestDto()
    {
      amount = (int)order.TotalAmount,
      msisdn = _zainCashOptions.msisdn,
      orderId = order.Id,
      serviceType = _zainCashOptions.serviceType,
      redirectUrl = _zainCashOptions.redirectUrl,
    };

    JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_zainCashOptions.secret));

    SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    Claim[] claims = new[]
    {
      new Claim("amount", zainCashPaymentRequestDto.amount.ToString()),
      new Claim("msisdn", zainCashPaymentRequestDto.msisdn),
      new Claim("orderId", zainCashPaymentRequestDto.orderId.ToString()),
      new Claim("serviceType", zainCashPaymentRequestDto.serviceType),
      new Claim("redirectUrl", zainCashPaymentRequestDto.redirectUrl),
      new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
      new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddHours(4).ToString()),
    };

    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials);

    string token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

    FormUrlEncodedContent data = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
    {
      new KeyValuePair<string, string>( "token", token),
      new KeyValuePair<string, string>( "merchantId", _zainCashOptions.merchantId),
      new KeyValuePair<string, string>( "lang", "ar"),
    });

    HttpResponseMessage response = await _httpClient.PostAsync
      (InitialzePaymentUrl, data);

    //Parse JSON response to Object
    string responsee = await response.Content.ReadAsStringAsync();

    JsonElement jsona = JsonSerializer.Deserialize<JsonElement>(responsee);

    string transactionID = jsona.GetProperty("id").GetString() ?? "";

    if (transactionID.IsNullOrEmpty())
      throw new Exception("missing transaction id");

    return PaymentUrl + transactionID;
  }

  public async Task<enPaymentStatus> ProcessPaymentCallBack(PaymentCallBackDto paymentCallBackReturnResponseDto)
  {
    Payment payment = PaymentCallBackDtoMapper.toEntity(paymentCallBackReturnResponseDto,
        (short)enPaymentMethod.ZainCash);

    try
    {
      payment.Id = await _paymentRepo.CreateAsync(payment);

      // Checking amount
      if (payment.Amount != (await _orderRepo.GetByIDAsync(paymentCallBackReturnResponseDto.OrderID))?.TotalAmount)
      {
        payment.Status = (short)enPaymentStatus.Failed;
        
        throw new ArgumentException("Transaction Amount is not the order amount");
      }

      return (enPaymentStatus)payment.Status;
    }
    catch (Exception ex)
    {
      Log.Logger.Error(nameof(ex), ex.Message);
      return (enPaymentStatus)payment.Status;
    }
  }

  public Task<bool> ValidateCallBack(string Body, string Signature)
  {
    throw new NotImplementedException();
  }
}
