using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.Core.Entities;
public class Customization
{
  public int ID { get; set; }
  public required int CustomizationChoiceID { get; set; }
  public required int ItemID { get; set; }

  [SetsRequiredMembers]
  public Customization(int CustomizationChoiceID, int ItemID)
  {
    this.CustomizationChoiceID = CustomizationChoiceID;
    this.ItemID = this.ItemID;
  }
  public Customization() { }
}
