using System.Configuration;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.DTOs.PaymentDtos;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.OrderItems;
using OnlineStore.Core.InterfacesAndServices.PaymentServices;
using OnlineStore.Infrastructure.Data.Models;
using OnlineStore.Infrastructure.DTOs;
using OnlineStore.Infrastructure.Mappers;
using OnlineStore.Infrastructure.Options;
using OpenQA.Selenium.BiDi.Input;
using Serilog;

namespace OnlineStore.Infrastructure.PaymentServices;
public class PayTabsPaymentService : IPaymentService
{
  private readonly PayTabsOptions _payTabsOptions;
  private readonly IListProductsWithQuantitiesService _listProductsWithQuantities;
  private readonly IPaymentRepo _paymentRepo;
  private readonly IOrderRepo _orderRepo;
  private readonly EStoreSystemContext _eStoreSystemContext;

  public PayTabsPaymentService(IListProductsWithQuantitiesService listProductsWithQuantitiesService,
    IOptions<PayTabsOptions> payTabsOptions,
    IPaymentRepo paymentRepo, IOrderRepo orderRepo, EStoreSystemContext eStoreSystemContext)
  {
    _payTabsOptions = payTabsOptions.Value;
    _listProductsWithQuantities = listProductsWithQuantitiesService;
    _paymentRepo = paymentRepo;
    _orderRepo = orderRepo;
    _eStoreSystemContext = eStoreSystemContext;
  }

  public async Task<string> GenereateTransactionURL(Order order)
  {
    try
    {
      User? user = _eStoreSystemContext.Users.Where(u => u.Id == order.Customer.UserId).FirstOrDefault();

      CustomerDetailsDto customerDetailsDto = CustomerDetailsMapper.toDto(user ?? 
        throw new ArgumentException("No user linked to the order: " + order.Id));

      if (string.IsNullOrEmpty(_payTabsOptions.server_key)) throw new ConfigurationErrorsException("Server key is not provided.");

      List<ProductNameQuantity> OrderItemNames =
        _listProductsWithQuantities.listProductsWithQuantitiesAsync(order.Id);

      string content = JsonSerializer.Serialize(new PaytabPaymentRequestDto()
      {
        ProfileId = _payTabsOptions.profile_id,
        TranType = _payTabsOptions.tran_type,
        TranClass = _payTabsOptions.tran_class,
        CartId = order.Id.ToString(),
        CartCurrency = _payTabsOptions.cart_currency,
        CartAmount = order.TotalAmount,
        CartDescription = JsonSerializer.Serialize(OrderItemNames),
        customerDetailsDto = customerDetailsDto,
        CallBack = _payTabsOptions.callback,
        PaypageLang = "en",
      });

      HttpClient httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("authorization", _payTabsOptions.server_key);

      httpClient.BaseAddress = new Uri(_payTabsOptions.BaseAddress);

      HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("payment/request",
        new StringContent(content));

      if (httpResponseMessage.IsSuccessStatusCode)
      {
        JsonElement jsonElement = await httpResponseMessage.Content.ReadFromJsonAsync<JsonElement>();
        return jsonElement.GetProperty("redirect_url").GetString()!;
      }
      return "";
    }
    catch
    (Exception ex)
    {
      Log.Logger.Error(nameof(ex), ex.Message);
      return "";
    }
  }

  public async Task<enPaymentStatus> ProcessPaymentCallBack(PaymentCallBackDto pcDto)
  {
    try
    {
      Payment payment = PaymentCallBackDtoMapper.toEntity(pcDto, (short)enPaymentMethod.MasterVisa);

      payment.Id = await _paymentRepo.CreateAsync(payment);

      await validatePaymentAsync(payment);

      return (enPaymentStatus)payment.Status;
    }
    catch (Exception ex)
    {
      Log.Logger.Error(nameof(ex), ex.Message);
      return enPaymentStatus.Failed;
    }
  }

  public Task<bool> ValidateCallBack(string Body, string Signature)
  {
    using (HMACSHA256 hMACSHA = new HMACSHA256(Encoding.UTF8.GetBytes(_payTabsOptions.server_key)))
    {
      byte[] HashedResults = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(Body));

      if (string.Equals(Convert.ToHexString(HashedResults), Signature, StringComparison.OrdinalIgnoreCase))
      {
        return Task.FromResult(true);
      }
      else
        return Task.FromResult(false);
    }
  }

  private async Task validatePaymentAsync(Payment payment)
  {
    Order order = await _orderRepo.GetByIDAsync(payment.OrderId) ?? 
      throw new ArgumentException($"Order not found, \n\ttransaction ID: {payment.OrderId}.");

    if (payment.Amount != order.TotalAmount)
    {
      payment.Status = (short)enPaymentStatus.Failed;
    }

  }
}
