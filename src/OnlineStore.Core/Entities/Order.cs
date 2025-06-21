using System.ComponentModel;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.Entities;
public class Order
{
  public int ID { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public int ShippingAddressID { get; set; }
  public int CustomerID { get; set; }
  public decimal TotalAmount { get; set; }
  public int OrderStatus { get; set; }

  public Order(int shippingAddressID, int customerID)
  {
    this.CreatedAt = DateTime.Now;
    this.ShippingAddressID = shippingAddressID;
    this.CustomerID = customerID;
    this.OrderStatus = (int) enOrderStatuses.Pending;
  }

  public Order()
  {  }

  public void Cancel()
  {
    OrderStatus = (int)enOrderStatuses.Cancelled;
  }

}
