using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs.Parameters;
public class UpdateProductParameters
{
  public int Id { get; set;}
  public required string Name { get; set;}
  public required string? Description { get; set;}
  public decimal Price { get; set;}
  public required string SKU { get; set;}
  public int Category_Id { get; set;}
}
