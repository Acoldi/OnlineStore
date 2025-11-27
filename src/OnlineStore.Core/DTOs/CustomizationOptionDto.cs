using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs;
public class CustomizationOptionDto
{
  public string Label { get; set; } = null!;

  public int TypeId { get; set; }

  public int ProductId { get; set; }

  public decimal AdditionalCost { get; set; }

}
