using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.Entities;
public class Order
{
    public int ID { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int ShippingAddressID { get; set; }
    public int CustomerID { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus{ get; set; } = enOrderStatuses.Pending.ToString();
}
