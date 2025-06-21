using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Entities;
public class CustomizationOptionType
{
  public int ID { get; set; }
  public required string TypeName { get; set; }

  public CustomizationOptionType(string typename)
  {
    this.TypeName = typename;
  }

  public CustomizationOptionType() { }
}
