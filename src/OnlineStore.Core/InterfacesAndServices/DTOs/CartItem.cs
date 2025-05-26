using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.InterfacesAndServices.DTOs;
public class CartItem
{
  public int ProductID { get; set; }
  public int Quantity { get; set; }
  public decimal PriceSnapShot { get; set; }
}
