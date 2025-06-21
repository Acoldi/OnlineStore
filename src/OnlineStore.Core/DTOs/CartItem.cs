using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs;
public class CartItem
{
  public required int ProductID { get; set; }
  public required int Quantity { get; set; }
  public List<int>? ChoicesID { get; set; }
}
