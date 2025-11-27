using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string Sku { get; set; } = null!;

    public int CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Slug { get; set; }

    public bool? IsActive { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();

    public virtual ICollection<CustomizationOption> CustomizationOptions { get; set; } = new List<CustomizationOption>();
}
