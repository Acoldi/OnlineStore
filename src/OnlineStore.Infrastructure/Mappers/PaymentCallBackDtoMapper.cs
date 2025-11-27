using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs.PaymentDtos;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.Mappers;
using OpenQA.Selenium.Internal;

namespace OnlineStore.Infrastructure.Mappers;
public class PaymentCallBackDtoMapper : Mapper<Payment, PaymentCallBackDto>
{
  public static PaymentCallBackDto toDto(Payment entity)
  {
    return new PaymentCallBackDto()
    {
      CartAmount = entity.Amount,
      PaymemtStatus = (enPaymentStatus)entity.Status,
      OrderID = entity.OrderId,
      TransactionID = entity.TransactionId,
    };
  }

  public static Payment toEntity(PaymentCallBackDto dto)
  {
    return new Payment()
    {
      Amount = dto.CartAmount,
      Status = (short)dto.PaymemtStatus,
      OrderId = dto.OrderID,
      TransactionId = dto.TransactionID,
      CreatedAt = DateTime.Now,
    };
  }

  public static Payment toEntity(PaymentCallBackDto dto, short method)
  {
    return new Payment()
    {
      Amount = dto.CartAmount,
      Status = (short)dto.PaymemtStatus,
      OrderId = dto.OrderID,
      TransactionId = dto.TransactionID,
      CreatedAt = DateTime.Now,
      Method = method,
    };
  }

}
