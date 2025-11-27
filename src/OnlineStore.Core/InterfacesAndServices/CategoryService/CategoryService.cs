using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.Mappers;

namespace OnlineStore.Core.InterfacesAndServices.CategoryService;
public class CategoryService : ICategoryService
{
  private readonly ICategoryRepo _categoryRepo;
  public CategoryService(ICategoryRepo categoryRepo)
  {
    _categoryRepo = categoryRepo;
  }

  public async Task<int> CreateAsync(CategoryDto newCategory)
  {
    newCategory.Name = UtilityService.GenerateSlug(newCategory.Name);
    return await _categoryRepo.CreateAsync(CategoryMapper.toEntity(newCategory));
  }

  public async Task<bool> DeleteAsync(int categoryID)
  {
    return await _categoryRepo.DeleteAsync(categoryID);
  }

  public async Task<bool> DeleteAsync(string categoryName, CancellationToken ct = default)
  {
    ct.ThrowIfCancellationRequested();

    return await _categoryRepo.DeleteCategoryByNameAsync(categoryName, ct);
  }

  public async Task<List<CategoryDto>?> ListAllAsync()
  {
    List<Category>? categories = await _categoryRepo.GetAsync();

    List<CategoryDto> categoryDtos = new List<CategoryDto>();

    if (categories == null) return null;

    foreach (var item in categories)
    {
      categoryDtos.Add(CategoryMapper.toDto(item));
    }

    return categoryDtos;
  }

  public async Task<List<CategoryDto>?> ListUnderAsync(string parentCategoryName)
  {
    List<Category>? categories = await _categoryRepo.GetCategoriesUnderParentNameAsync(parentCategoryName, null);

    List<CategoryDto> categoryDtos = new List<CategoryDto>();

    if (categories == null) return null;

    foreach (var item in categories)
    {
      categoryDtos.Add(CategoryMapper.toDto(item));
    }

    return categoryDtos;
  }

  public async Task<List<CategoryDto>?> ListUnderAsync(int parentCategoryID)
  {
    List<Category>? categories = await _categoryRepo.GetCategoriesUnderParentIDAsync(parentCategoryID, null);


    List<CategoryDto> categoryDtos = new List<CategoryDto>();

    if (categories == null) return null;

    foreach (var item in categories)
    {
      categoryDtos.Add(CategoryMapper.toDto(item));
    }

    return categoryDtos;
  }

  public Task<bool> UpdateAsync(CategoryDto categoryDto)
  {
    return _categoryRepo.UpdateAsync(CategoryMapper.toEntity(categoryDto));
  }
}
