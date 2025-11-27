using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs;
public class ProductDto
{
  public string Name { get; set; } = null!;

  public string? Description { get; set; }

  public decimal Price { get; set; }

  public string Sku { get; set; } = null!;

  public int CategoryId { get; set; }

  public DateTime CreatedAt { get; set; }

  public string? Slug { get; set; }

  public bool? IsActive { get; set; }
}
