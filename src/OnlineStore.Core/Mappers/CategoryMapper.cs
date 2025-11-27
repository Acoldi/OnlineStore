using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.Mappers;
public class CategoryMapper : Mapper<Category, CategoryDto>
{
  public static Category toEntity(CategoryDto categoryDto)
  {
    return new Category()
    {
      Name = categoryDto.Name,
      ParentCategoryId = categoryDto.ParentCategoryId,
      Slug = categoryDto.Slug,
    };
  }

  public static CategoryDto toDto(Category category)
  {
    return new CategoryDto()
    {
      Name = category.Name,
      ParentCategoryId = category.ParentCategoryId,
      Slug = category.Slug,
    };
  }


}
