using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs.Parameters;
public class AddToCartParam
{
  public required int ProductID { get; set; }
  public int ShoppingCartID { get; set; }
  public int Quantity { get; set; } = 1;
}
