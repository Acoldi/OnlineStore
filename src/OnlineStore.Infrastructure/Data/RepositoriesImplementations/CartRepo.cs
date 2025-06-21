using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class CartRepo : ICartRepo
{
  SqlConnection _connection;
  public CartRepo(IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
  }

  public Task<int> CreateAsync(ShoppingCart UserID, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public async Task<int?> CreateAsync(Guid UserID, CancellationToken? cancellationToken)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException();

    return await _connection.QuerySingleOrDefaultAsync<int?>("SP_CreateShoppingCart", commandType: System.Data.CommandType.StoredProcedure,
      param: new { UserID });
  }

  public Task<bool> DeleteAsync(int ID, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public Task<List<ShoppingCart>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public Task<ShoppingCart?> GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public async Task<List<OrderItem>?> GetCartItemsAsync(Guid UserID)
  {
    return [.. (await _connection.QueryAsync<OrderItem>("SP_GetCartItems", commandType: System.Data.CommandType.StoredProcedure,
      param: new { UserID }))];
  }

  public async Task<bool> RemoveCartItemsAsync(int ShoppingCartID)
  {
    return await _connection.ExecuteAsync("SP_DeleteItemsFromCart", commandType: System.Data.CommandType.StoredProcedure
      , param: new { ShoppingCartID }) > 0;
  }

  public Task<bool> UpdateAsync(ShoppingCart param, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }
}
