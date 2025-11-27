using Microsoft.Identity.Client;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.DTOs.PaymentDtos;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.InterfacesAndServices.PaymentServices;
public interface IPaymentService
{
  public Task<string> GenereateTransactionURL(Order order,
    CustomerDetailsDto? CustomerDetails);

  public Task<enPaymentStatus> ProcessPaymentCallBack(PaymentCallBackDto paymentCallBackReturnResponseDto);

  public Task<bool> ValidateCallBack(string Body, string Signature);
}
