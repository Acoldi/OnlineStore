using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.InterfacesAndServices.DTOs.Parameters;
public class CreateOrderParam
{
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public required int ShippingAddressID { get; set; }
  public required int CustomerID { get; set; }
  public required decimal TotalAmount { get; set; }
  public string OrderStatus { get; set; } = enOrderStatuses.Pending.ToString();
}
