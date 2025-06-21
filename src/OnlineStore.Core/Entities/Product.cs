namespace OnlineStore.Core.Entities;
public class Product
{
  public int ID { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public decimal Price { get; set; }
  public required string SKU { get ; set; }
  public int CategoryID { get; set; }
    public int? CustomizationOptionID { get; set; }
  public DateTime CreatedAt { get; set; }
  public bool IsActive { get; set; }
  public required string SLUG { get; set; }
  

  public Product(int id, string name, string? description, decimal price, int categoryId, string sku, DateTime createdAt, bool isActive, 
    string SLUG, int customizationOptionID)
  {
    ID = id;
    Name = name;
    Description = description;
    Price = price;
    CategoryID = categoryId;
    SKU = sku;
    CreatedAt = createdAt;
    IsActive = isActive;
    this.SLUG = SLUG;
    CustomizationOptionID = customizationOptionID;
  }

  public Product()
  { }

}
