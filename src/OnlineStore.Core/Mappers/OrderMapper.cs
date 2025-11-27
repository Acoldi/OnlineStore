using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.Mappers;
public class OrderMapper : Mapper<Order, OrderDto>
{
  public static OrderDto toDto(Order Entity)
  {
    return new OrderDto()
    {
      CreatedAt = Entity.CreatedAt,
      CustomerId = Entity.CustomerId,
      OrderStatus = Entity.OrderStatus,
      ShippingAddressId = Entity.ShippingAddressId,
      TotalAmount = Entity.TotalAmount
    };
  }

  public static Order toEntity(OrderDto DTO)
  {
    return new Order()
    {
      CreatedAt = DTO.CreatedAt,
      CustomerId = DTO.CustomerId,
      OrderStatus = DTO.OrderStatus,
      ShippingAddressId = DTO.ShippingAddressId,
      TotalAmount = DTO.TotalAmount
    };
  }
}
