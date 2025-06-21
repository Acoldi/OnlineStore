using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface ICategoryRepo : IDataAccess<Category, int>
{
  public Task<List<Category>?> GetCategoriesUnderParentIDAsync(int ParentCategoryID, CancellationToken? ct);
  public Task<List<Category>?> GetCategoriesUnderParentNameAsync(string Name, CancellationToken? ct);
  public Task<bool> DeleteCategoryByNameAsync(string Name, CancellationToken? ct);
}
