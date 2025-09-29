using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.Entities;
public class Order
{
  public int ID { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public int ShippingAddressID { get; set; }
  public int CustomerID { get; set; }
  public decimal? TotalAmount { get; set; }
  public enOrderStatuses OrderStatus { get; set; }

  public List<string>? RelatedCategories { get; set; }

  public Order(int shippingAddressID, int customerID)
  {
    this.CreatedAt = DateTime.UtcNow;
    this.ShippingAddressID = shippingAddressID;
    this.CustomerID = customerID;
    this.OrderStatus = enOrderStatuses.Pending;
    TotalAmount = null;
  }

  public Order()
  {  }


}
