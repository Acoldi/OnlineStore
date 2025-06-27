using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.Core.Entities;
public class CustomizationChoice
{
  public int ID { get; set; }
  public required int OptionID { get; set; }
  public required string Value { get; set; }
  public required decimal AdditionalCost { get; set; }

  [SetsRequiredMembers]
  public CustomizationChoice(int optionId, string value, decimal additionalCost)
  {
    OptionID = optionId;
    Value = value;
    AdditionalCost = additionalCost;
  }

  public CustomizationChoice() { }
}
