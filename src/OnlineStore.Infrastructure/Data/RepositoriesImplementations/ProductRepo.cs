using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class ProductRepo : IProductRepo
{
  SqlConnection _connection;
  public ProductRepo(IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
  }

  public async Task<Product?> GetByNameAsync(string Name, CancellationToken? cancellationToken = null)
  {
    try
    {
      return await _connection.QuerySingleAsync<Product>("SP_GetProductByName", commandType: CommandType.StoredProcedure,
        param: new
        {
          ProductName = Name
        });

    }
    catch
    {
      return null;
    }
  }

  public async Task<List<Product>?> GetByCategory(string category, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException();

    return (await _connection.QueryAsync<Product>("SP_GetProductByCategory", commandType:
      CommandType.StoredProcedure, param: new { category })).ToList();
  }
  public Task<int> GetIdBySKU(string ProductSKU, CancellationToken? ct = null)
  {
    throw new NotImplementedException();
  }

  public async Task<List<Product>?> GetCustomizableProducts(CancellationToken? cancellationToken)
  {
    return [.. (await _connection.QueryAsync<Product>("SP_GetCustomizableProducts", commandType: CommandType.StoredProcedure))];
  }

  public async Task<string> GetProductSKU(string ProductName, CancellationToken? cancellationToken)
  {
    return await _connection.QuerySingleAsync<string>("[SP_GetProductSKU]", commandType:
      CommandType.StoredProcedure, param: new { ProductName });
  }

  public async Task<List<Product>?> GetByCategoryID(int ID, CancellationToken? ct = null)
  {
    return (await _connection.QueryAsync<Product>("SP_GetProductByCategoryID",
    commandType: CommandType.StoredProcedure, param: new { ID })).ToList();
  }

  public async Task<List<Product>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException();

    return (await _connection.QueryAsync<Product>("SP_GetAllProducts", commandType: CommandType.StoredProcedure)).ToList();

  }

  public async Task<Product?> GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException();

    return await _connection.QuerySingleAsync<Product>("SP_GetProductByID", commandType: CommandType.StoredProcedure, param: new { ID });
  }

  public async Task<int> CreateAsync(Product param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException();

    return await _connection.QuerySingleAsync<int>("SP_AddProduct", commandType: CommandType.StoredProcedure,
    param: param);

  }

  public async Task<bool> UpdateAsync(Product param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException();

    return await _connection.ExecuteAsync("SP_UpdateProduct", commandType: CommandType.StoredProcedure, param: param) == 1;
  }

  public async Task<bool> DeleteAsync(int ID, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException();

    return await _connection.ExecuteAsync("SP_DeleteProduct", commandType: CommandType.StoredProcedure, param: new { ID }) == 1;

  }
}
