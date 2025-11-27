using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs.Items;
public class OrderItemDto
{
  public int ProductId { get; set; }
  public int Quantity { get; set; }
  public required int OrderItemID { get; set; }

  public List<int> ChoicesID {  get; set; } = new List<int>();
  //public List<int> ChoicesID { get; set; } = [];
}
