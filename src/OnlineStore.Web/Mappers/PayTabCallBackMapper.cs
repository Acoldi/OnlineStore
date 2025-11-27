using OnlineStore.Core.DTOs.PaymentDtos;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Web.Mappers;

public class PayTabCallBackMapper
{
  public static PaymentCallBackDto PaytabToPaymentDto(PayTabCallBackDto payTabCallBackRequestDto)
  {
    PaymentCallBackDto paymentCallBackDto = new PaymentCallBackDto()
    {
      TransactionID = payTabCallBackRequestDto.TranRef,
      OrderID = int.Parse(payTabCallBackRequestDto.CartId),
      CartCurrency = payTabCallBackRequestDto.CartCurrency.ToString(),
      CartAmount = payTabCallBackRequestDto.CartAmount,
      CustomerDetails = new CustomerDetailsDto()
      {
        city = payTabCallBackRequestDto.CustomerDetails.City,
        country = payTabCallBackRequestDto.CustomerDetails.Country,
        email = payTabCallBackRequestDto.CustomerDetails.Email,
        name = payTabCallBackRequestDto.CustomerDetails.Name,
        phone = payTabCallBackRequestDto.CustomerDetails.Phone,
        state = payTabCallBackRequestDto.CustomerDetails.State,
        street1 = payTabCallBackRequestDto.CustomerDetails.Street1,
      },
    };

    switch (payTabCallBackRequestDto.PaymentResult.ResponseStatus)
    {
      case ResponseStatus.A:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Authorized;
        break;
      case ResponseStatus.H:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Pending;
        break;
      case ResponseStatus.P:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Pending;
        break;
      case ResponseStatus.V:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Cancelled;
        break;
      case ResponseStatus.E:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Failed;
        break;
      case ResponseStatus.D:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Failed;
        break;
      case ResponseStatus.X:
        paymentCallBackDto.PaymemtStatus = Core.Enums.enPaymentStatus.Expired;
        break;
    }

    return paymentCallBackDto;
  }
}
