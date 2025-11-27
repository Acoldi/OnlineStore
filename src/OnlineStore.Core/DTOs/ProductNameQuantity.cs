using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs;
public class ProductNameQuantity
{
  public string Name { get; set; } = string.Empty;
  public decimal Quantity { get; set; }
}
