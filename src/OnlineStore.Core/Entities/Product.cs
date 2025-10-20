namespace OnlineStore.Core.Entities;

public partial class Product
{
  public int Id { get; set; }

  public required string Name { get; set; }

  public string? Description { get; set; }

  public decimal Price { get; set; }

  public required string Sku { get; set; }

  public int CategoryId { get; set; }

  public Category? Category { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.Now;

  public string? Slug { get; set; }

  public int? CustomizationOptionId { get; set; }

  public bool? IsActive { get; set; }

  public Product(int id, string name, string? description, decimal price, int categoryId, string sku, DateTime createdAt, bool isActive,
  string SLUG, int customizationOptionID, Category category)
  {
    Id = id;
    Name = name;
    Category = category;
    Description = description;
    Price = price;
    CategoryId = categoryId;
    Sku = sku;
    CreatedAt = createdAt;
    IsActive = isActive;
    Slug = SLUG;
    CustomizationOptionId = customizationOptionID;
  }

  public Product() { }
}
