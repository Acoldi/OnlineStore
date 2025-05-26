using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.InterfacesAndServices.DTOs.Parameters;
public class CreateOrderItemParam
{
  public required int ProductID { get; set; }
  public int OrderID { get; set; }
  public int ShoppingCartID { get; set; }
  public int Quantity { get; set; } = 1;
  public required decimal Price { get; set; }
}
