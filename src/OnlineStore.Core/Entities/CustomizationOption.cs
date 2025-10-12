namespace OnlineStore.Core.Entities;
public class CustomizationOption
{
  public int ID { get; set; }
  public required string Label { get; set; }
  public required int TypeID { get; set; }
  public CustomizationOptionType? customizationOptionType { get; set; }
  public int ProductID { get; set; }
  public decimal AdditionalCost { get; set; }

  public CustomizationOption(string label, int typeID, int productID, decimal additionalCost)
  {
    Label = label;
    TypeID = typeID;
    ProductID = productID;
    AdditionalCost = additionalCost;
  }

  public CustomizationOption() { }
}
