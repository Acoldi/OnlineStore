using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.DTOs;
public class OrderDto
{
  public DateTime CreatedAt { get; set; }

  public required int ShippingAddressId { get; set; }

  public int CustomerId { get; set; }

  public decimal TotalAmount { get; set; }

  /// <summary>
  ///   Pending = 1,
  ///   Shipping,
  ///   Delivered,
  ///   Cancelled
  /// </summary>
  public short OrderStatus { get; set; }

}
