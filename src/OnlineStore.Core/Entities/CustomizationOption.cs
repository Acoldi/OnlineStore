using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Entities;
public class CustomizationOption
{
  public int ID { get; set; }
  public required string Label { get; set; }
  public required int TypeID { get; set; }
  public int ProductID { get; set; }

  [SetsRequiredMembers]
  public CustomizationOption(string label, int typeID, int productID)
  {
    Label = label;
    TypeID = typeID;
    ProductID = productID;
  }

  public CustomizationOption() { }
}
