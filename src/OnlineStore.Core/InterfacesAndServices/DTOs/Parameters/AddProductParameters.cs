using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Interfaces.DTOs.Parameters;
public class AddProductParameters
{
  public required string ProductName { get; set; }
  public string? Description { get; set; }
  public decimal Price { get; set; }
  public required string? Sku { set; get; }
  public int CategoryID { get; set;}
  public DateTime CreatedDate { get; set; }
}
