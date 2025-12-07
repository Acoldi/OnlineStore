using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Infrastructure.Data.Models;
using OpenQA.Selenium.DevTools;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class CartRepo : ICartRepo
{
  IConnectionFactory _connectionFactory;
  private readonly EStoreSystemContext _context;
  public CartRepo(EStoreSystemContext eStoreSystemContext, IConnectionFactory connectionFactory)
  {
    _connectionFactory = connectionFactory;
    _context = eStoreSystemContext;
  }

  public async Task<int> CreateAsync(ShoppingCart ShopingCart, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    await _context.ShoppingCarts.AddAsync(ShopingCart);

    return await _context.SaveChangesAsync();
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
    try
    {

      if (cancellationToken?.IsCancellationRequested == true)
        throw new OperationCanceledException();
      using SqlConnection _connection = await _connectionFactory.CreateSqlConnection();

      return await _connection.QuerySingleOrDefaultAsync<int>("SP_CreateShoppingCart", commandType: System.Data.CommandType.StoredProcedure,
        param: new { UserID });
    }
    catch (Exception ex)
    {
      Serilog.Log.Logger.Error(ex, ex.Message);
      return 0;
    }
  }

  public async Task<bool> DeleteAsync(int ID, CancellationToken? cancellationToken = null)
  {
    return await _context.ShoppingCarts.Where(sc => sc.Id == ID).ExecuteDeleteAsync() > 0;
  }

  public async Task<List<ShoppingCart>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    return await _context.ShoppingCarts.ToListAsync();
  }

  public async Task<ShoppingCart?> GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    return await _context.ShoppingCarts.Where(sc => sc.Id == ID).FirstOrDefaultAsync();
  }

  public async Task<List<OrderItem>?> GetCartItemsAsync(Guid UserID)
  {
    using SqlConnection _connection = await _connectionFactory.CreateSqlConnection();

    return [.. (await _connection.QueryAsync<OrderItem>("SP_GetCartItems", commandType: System.Data.CommandType.StoredProcedure,
      param: new { UserID }))];

  }

  public async Task<bool> RemoveCartItemsAsync(int ShoppingCartID)
  {
    try
    {
      using SqlConnection _connection = await _connectionFactory.CreateSqlConnection();
      // This should be related to order items repo
      await _connection.ExecuteAsync("SP_DeleteItemsFromCart", commandType: System.Data.CommandType.StoredProcedure
        , param: new { ShoppingCartID });
      return true;
    }
    catch (Exception ex)
    {
      Serilog.Log.Logger.Error(ex, ex.Message);
      throw;
    }
  }

  public async Task<bool> UpdateAsync(ShoppingCart param, CancellationToken? cancellationToken = null)
  {
    return await _context.ShoppingCarts.ExecuteUpdateAsync(sc =>
    sc.SetProperty(p => p.UserId, param.UserId).SetProperty(p => p.User, param.User).
    SetProperty(p => p.OrderItems, param.OrderItems)) > 0;
  }

  public async Task<bool> RemoveCartItemsAsync(List<int> ItemIDs, int ShoppingCartID)
  {
    // I can either use dapper (TVP - table valued parameters) or EF, I will try EF

    return await _context.OrderItems.Where(o => ItemIDs.Contains(o.Id) && o.ShoppingCartId == ShoppingCartID).ExecuteDeleteAsync()
      > 0;
  }
}
