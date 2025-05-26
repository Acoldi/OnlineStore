using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Interfaces.Category;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.Category;
public class CategoryService : ICategoryService
{
  IDataAccess _dataAccess;
  public CategoryService(IDataAccess dataAccess)
  {
    _dataAccess = dataAccess;
  }

  public async Task<bool> DeleteCategoryAsync(int categoryID, CancellationToken ct)
  {
     return await _dataAccess.DeleteAsync("SP_DeleteCategroy", System.Data.CommandType.StoredProcedure, ct, categoryID);
  }
  
  public async Task<bool> DeleteCategoryByNameAsync(string Name, CancellationToken ct)
  {
     return await _dataAccess.DeleteAsync("SP_DeleteCategroy", System.Data.CommandType.StoredProcedure, ct, Name);
  }

  public async Task<List<Entities.Category>?> GetCategoriesAsync(CancellationToken ct)
  {
    return await _dataAccess.LoadDataAsync<Entities.Category>("SP_GetCategories", System.Data.CommandType.StoredProcedure, ct);
  }
  public async Task<List<Entities.Category>?> GetCategoryByName(string Name, CancellationToken ct)
  {
    return await _dataAccess.LoadDataAsync<Entities.Category>("SP_GetCategoryByID", System.Data.CommandType.StoredProcedure, ct, new { Name });
  }

  public async Task<List<Entities.Category>?> GetCategoriesUnderAsync(int ParentCategoryID, CancellationToken ct)
  {
    return await _dataAccess.LoadDataAsync<Entities.Category>("SP_GetCategoriesUnder", System.Data.CommandType.StoredProcedure, ct, ParentCategoryID);
  }
  public async Task<List<Entities.Category>?> GetCategoriesUnderAsync(string ParentCategoryName, CancellationToken ct)
  {
    return await _dataAccess.LoadDataAsync<Entities.Category>("SP_GetCategoriesUnderParentName", System.Data.CommandType.StoredProcedure, ct, ParentCategoryName);
  }
  public async Task<bool> UpdateCategoryAsync(int ID, Entities.Category NewCategory, CancellationToken ct)
  {
    return await _dataAccess.UpdateDataAsync("SP_UpdateCategory", System.Data.CommandType.StoredProcedure, ct,
      new { NewCategory.ID, NewCategory.Name, NewCategory.ParentCategoryID, NewCategory.Slug });
  }
  public async Task<int> CreatCategoryAsync(Entities.Category category)
  {
    return await _dataAccess.CreateAsync("SP_AddCategory", System.Data.CommandType.StoredProcedure,
                            cancellationToken: null, new { category.Name, category.ParentCategoryID, category.Slug});
  }

}
