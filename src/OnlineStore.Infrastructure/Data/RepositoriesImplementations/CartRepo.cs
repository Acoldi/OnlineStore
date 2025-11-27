using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class CartRepo : ICartRepo
{
  SqlConnection _connection;
  private readonly EStoreSystemContext _context;
  public CartRepo(EStoreSystemContext eStoreSystemContext, IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
    _context = eStoreSystemContext;
  }

  public Task<int> CreateAsync(ShoppingCart UserID, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="UserID"></param>
  /// <param name="cancellationToken"></param>
  /// <returns>This method returns the cart id if it's already there, else it creates a new one and returns its id.</returns>
  /// <exception cref="OperationCanceledException"></exception>
  public async Task<int> CreateAsync(Guid UserID, CancellationToken? cancellationToken)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException();

    return await _connection.QuerySingleOrDefaultAsync<int>("SP_CreateShoppingCart", commandType: System.Data.CommandType.StoredProcedure,
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
    // This should be related to order items repo
    return await _connection.ExecuteAsync("SP_DeleteItemsFromCart", commandType: System.Data.CommandType.StoredProcedure
      , param: new { ShoppingCartID }) > 0;
  }

  public Task<bool> UpdateAsync(ShoppingCart param, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public async Task<bool> RemoveCartItemsAsync(List<int> ItemIDs, int ShoppingCartID)
  {
    // I can either use dapper (TVP - table valued parameters) or EF, I will try EF

    return await _context.OrderItems.Where(o => ItemIDs.Contains(o.Id) && o.ShoppingCartId == ShoppingCartID).ExecuteDeleteAsync() 
      > 0;
  }
}
