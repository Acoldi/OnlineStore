using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.Core.Entities;
public class Customization
{
  public int ID { get; set; }
  public required int CustomizationChoiceID { get; set; }
  public required int ItemID { get; set; }
  public decimal AdditionalCost { get; set; }

  public Customization(int CustomizationChoiceID, int ItemID, decimal additionalCost)
  {
    this.CustomizationChoiceID = CustomizationChoiceID;
    this.ItemID = this.ItemID;
    AdditionalCost = additionalCost;
  }
  public Customization() { }
}
