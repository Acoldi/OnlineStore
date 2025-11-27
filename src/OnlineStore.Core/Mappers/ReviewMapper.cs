using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.Mappers;
public class ReviewMapper : Mapper<Review, ReviewDto>
{
  public static ReviewDto toDto(Review Entity)
  {
    return new ReviewDto()
    {
      Comment = Entity.Comment,
      CreatedAt = Entity.CreatedAt,
      ProductId = Entity.ProductId,
      Rating = Entity.Rating,
      UserId = Entity.UserId,
    };
  }

  public static Review toEntity(ReviewDto DTO)
  {
    return new Review()
    {
      Comment = DTO.Comment,
      CreatedAt = DTO.CreatedAt,
      ProductId = DTO.ProductId,
      Rating = DTO.Rating,
      UserId = DTO.UserId,
    };
  }
}
