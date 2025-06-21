using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs.Parameters;
public class AddProductParameters
{
  public required string ProductName { get; set; }
  public string? Description { get; set; }
  public decimal Price { get; set; }
  public int CategoryID { get; set;}
  public int? CustomizationOptionID { get; set; }
}
