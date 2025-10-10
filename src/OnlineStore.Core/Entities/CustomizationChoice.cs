using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.Core.Entities;
public class CustomizationChoice
{
  public int ID { get; set; }
  public required int OptionID { get; set; }
  public required string Value { get; set; }

  public CustomizationChoice(int optionId, string value)
  {
    OptionID = optionId;
    Value = value;
  }

  public CustomizationChoice() { }
}
