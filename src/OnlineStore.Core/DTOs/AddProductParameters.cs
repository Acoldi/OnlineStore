using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;

namespace OnlineStore.Web.DTOs;
public class AddProductParameters
{
  public required string ProductName { get; set; }
  public string? Description { get; set; }
  public decimal Price { get; set; }
  public int CategoryID { get; set;}
  public required Category category { get; set; }
  public int? CustomizationOptionID { get; set; }
}
