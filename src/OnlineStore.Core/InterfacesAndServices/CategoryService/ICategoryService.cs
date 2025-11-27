using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.InterfacesAndServices.CategoryService;
public interface ICategoryService
{
  public Task<int> CreateAsync(CategoryDto newCategory);

  public Task<bool> UpdateAsync(CategoryDto newCategory);

  public Task<bool> DeleteAsync(int categoryID);
  public Task<bool> DeleteAsync(string categoryID, CancellationToken ct = default);

  public Task<List<CategoryDto>?> ListAllAsync();

  public Task<List<CategoryDto>?> ListUnderAsync(string parentCategoryName);

  public Task<List<CategoryDto>?> ListUnderAsync(int parentCategoryID);
}
