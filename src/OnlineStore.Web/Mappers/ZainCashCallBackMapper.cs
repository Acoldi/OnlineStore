using System.Configuration;
using Microsoft.Identity.Client;
using OnlineStore.Core.DTOs.PaymentDtos;
using OnlineStore.Infrastructure.Options;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Web.Mappers;

public static class ZainCashCallBackMapper
{
  public static PaymentCallBackDto toPaymentDto(ZainCashCallBadkDto zainCashCallBadkDto, ZainCashOptions zainCashOptions)
  {
    PaymentCallBackDto paymentCallBackDto = new PaymentCallBackDto()
    {
      OrderID = zainCashCallBadkDto.orderId,
      CartCurrency = zainCashOptions.currency,
      TransactionID = zainCashCallBadkDto.transactionID,
    };

    switch (zainCashCallBadkDto.status)
    {
      case ZainCashPaymentStatus.pending:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Pending;
        break;
      case ZainCashPaymentStatus.success:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Processing;
        break;
      case ZainCashPaymentStatus.completed:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Completed;
        break;
      case ZainCashPaymentStatus.failed:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Failed;
        break;
      default:
        break;
    }
  
    return paymentCallBackDto;
  }
}
