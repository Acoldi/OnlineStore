using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.Core.Entities;
public class Customization
{
  public int ID { get; set; }
  public required int CustomizationChoiceID { get; set; }
  public required int OrderItemID { get; set; }
  public required decimal ExtraCost { get; set; }

  [SetsRequiredMembers]
  public Customization(int CustomizationChoiceID, int orderItemID, decimal extraCost)
  {
    this.CustomizationChoiceID = CustomizationChoiceID;
    OrderItemID = OrderItemID;
    ExtraCost = extraCost;
  }
  public Customization() { }
}
