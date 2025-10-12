using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.Core.Entities;
public class Customization
{
  public int ID { get; set; }
  public required int CustomizationChoiceID { get; set; }
  public required int OrderItemID { get; set; }

  public Customization(int CustomizationChoiceID, int OrderItemID)
  {
    this.CustomizationChoiceID = CustomizationChoiceID;
    this.OrderItemID = OrderItemID;
  }
  public Customization() { }
}
