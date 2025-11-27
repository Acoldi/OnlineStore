using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.Mappers;
public class ProductMapper : Mapper<Product, ProductDto>
{
  public static ProductDto toDto(Product Entity)
  {
    return new ProductDto()
    {
      Name = Entity.Name,
      CategoryId = Entity.CategoryId,
      CreatedAt = Entity.CreatedAt,
      Description = Entity.Description,
      IsActive = Entity.IsActive,
      Price = Entity.Price,
      Sku = Entity.Sku,
      Slug = Entity.Slug
    };
  }

  public static Product toEntity(ProductDto DTO)
  {
    return new Product()
    {
      Name = DTO.Name,
      CategoryId = DTO.CategoryId,
      CreatedAt = DTO.CreatedAt,
      Description = DTO.Description,
      IsActive = DTO.IsActive,
      Price = DTO.Price,
      Sku = DTO.Sku,
      Slug = DTO.Slug
    };
  }
}
