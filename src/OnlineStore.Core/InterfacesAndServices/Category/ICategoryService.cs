
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.Interfaces.Category;
public interface ICategoryService
{
  public Task<List<Entities.Category>?> GetCategoriesAsync(CancellationToken ct);
  public Task<List<Entities.Category>?> GetCategoriesUnderAsync(int ParentCategoryID, CancellationToken ct);
  public Task<List<Entities.Category>?> GetCategoriesUnderAsync(string Name, CancellationToken ct);

  public Task<int> CreatCategoryAsync(Entities.Category category);

  public Task<bool> DeleteCategoryAsync(int categoryID, CancellationToken ct);

  public Task<bool> DeleteCategoryByNameAsync(string Name, CancellationToken ct);

  public Task<bool> UpdateCategoryAsync(int ID, Entities.Category NewCategory, CancellationToken ct);

  public Task<List<Entities.Category>?> GetCategoryByName(string Name, CancellationToken ct);

}
