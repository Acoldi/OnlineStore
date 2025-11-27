using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.DTOs.Items;
public class CartItemDto
{
  public int ProductId { get; set; }
  public int Quantity { get; set; }
  public required int ShoppingCartId { get; set; }

  public List<int> ChoicesID = new List<int>();
}
