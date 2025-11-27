using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices.IRepositories;


namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class CategoryRepo : ICategoryRepo
{
  SqlConnection _connection;
  public CategoryRepo(IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
  }

  public async Task<int> CreateAsync(Category param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.QuerySingleOrDefaultAsync<int>("SP_AddCategory", commandType:
      CommandType.StoredProcedure, param: new { param.Name, param.ParentCategoryId, param.Slug });
  }

  public async Task<bool> UpdateAsync(Category param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.ExecuteAsync("SP_UpdateCategory", new { updatedCategory = param }, null, null, 
      CommandType.StoredProcedure) == 1;
  }

  public async Task<bool> DeleteAsync(int param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);
    try
    {
      return await _connection.ExecuteAsync("SP_DeleteCategroy", new { ID = param }, null, null, System.Data.CommandType.StoredProcedure) == 1;

    }
    catch (SqlException)
    {
      return false;
    }
  }

  public async Task<bool> DeleteCategoryByNameAsync(string Name, CancellationToken? ct)
  {
    if (ct?.IsCancellationRequested == true)
      throw new OperationCanceledException(ct.Value);

    return await _connection.ExecuteAsync("[dbo].[SP_DeleteCategroyByName]", commandType: CommandType.StoredProcedure, param: new { Name }) == 1;

  }

  public async Task<List<Category>?> GetAsync(CancellationToken? ct = null)
  {
    if (ct?.IsCancellationRequested == true)
      throw new OperationCanceledException(ct.Value);

    return (await _connection.QueryAsync<Category>("SP_GetCategories", commandType: CommandType.StoredProcedure)).ToList();
  }

  public async Task<Category?> GetByIDAsync(int param, CancellationToken? ct = null)
  {
    if (ct?.IsCancellationRequested == true)
      throw new OperationCanceledException(ct.Value);

    return await _connection.QuerySingleOrDefaultAsync<Category>("[dbo].[SP_GetCategoryByID]", param: param, commandType: CommandType.StoredProcedure);
  }

  public async Task<List<Category>?> GetCategoriesUnderParentNameAsync(string Name, CancellationToken? ct)
  {
    List<Category> results =  (await _connection.QueryAsync<Category>("SP_GetCategoriesByName",
      commandType: CommandType.StoredProcedure, param: new { Name })).ToList();

    if (results.Count == 0)
      return null;
    else return results;
  }

  public async Task<List<Category>?> GetCategoriesUnderParentIDAsync(int ParentCategoryID, CancellationToken? ct)
  {
    if (ct?.IsCancellationRequested == true)
      throw new OperationCanceledException(ct.Value);

    return (await _connection.QueryAsync<Category>("SP_GetCategoriesUnder", commandType: CommandType.StoredProcedure, param: new { ParentCategoryID })).ToList();
  }
}
