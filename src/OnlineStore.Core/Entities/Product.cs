namespace OnlineStore.Core.Entities;
public class Product
{
  int? ID;
  public string? Name { get; set; }
  public string? Description { get; set; }
  public decimal Price { get; set; }
  public int Category_Id { get; set; }
  public string? Sku { get; set; }
  public DateTime CreatedAt { get; set; }
  public bool IsActive { get; set; }

  public Product(int? id, string name, string? description, decimal price, int categoryId, string? sku, DateTime createdAt, bool isActive)
  {
    ID = id;
    Name = name;
    Description = description;
    Price = price;
    Category_Id = categoryId;
    Sku = sku;
    CreatedAt = createdAt;
    IsActive = isActive;
  }

  public Product()
  {  }
}
