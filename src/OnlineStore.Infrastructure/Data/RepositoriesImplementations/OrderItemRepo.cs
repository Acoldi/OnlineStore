using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Infrastructure.Data.Models;
using Serilog;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class OrderItemRepo : IOrderItemRepo
{
  EStoreSystemContext _context;
  IConnectionFactory _connectionFactory;
  public OrderItemRepo(EStoreSystemContext context, IConnectionFactory connectionFactory)
  {
    _connectionFactory = connectionFactory;
    _context = context;
  }

  public async Task<int> CreateAsync(OrderItem param, CancellationToken? ct = null)
  {
    if (ct?.IsCancellationRequested == true)
      throw new InvalidOperationException(ct.Value.ToString());

    using (SqlConnection sqlConnection = await _connectionFactory.CreateSqlConnection())
    {
      return await sqlConnection.QuerySingleOrDefaultAsync<int>("SP_AddOrderItemToCart", param:
          new
          {
            param.ProductId,
            param.ShoppingCartId,
            param.Quantity,
          }, commandType: System.Data.CommandType.StoredProcedure);
    }
  }

  /// <summary>
  /// Adds a list of order items
  /// </summary>
  /// <param name="OrderItems"></param>
  /// <param name="ct"></param>
  /// <returns>The number of added orderItems</returns>
  /// <exception cref="InvalidOperationException"></exception>
  public async Task<int> CreateAsync(List<OrderItem> OrderItems, CancellationToken? ct = null)
  {
    if (ct?.IsCancellationRequested == true)
      throw new InvalidOperationException(ct.Value.ToString());

    int inserted = 0;
    using (SqlConnection sqlConnection = await _connectionFactory.CreateSqlConnection())
    {

      foreach (OrderItem orderItem in OrderItems)
      {

        inserted += await sqlConnection.ExecuteAsync("SP_AddOrderItemToCart", param:
            new
            {
              orderItem.ProductId,
              orderItem.ShoppingCartId,
              orderItem.Quantity,
            }, commandType: System.Data.CommandType.StoredProcedure);
      }
    }
    return inserted;
  }

  public async Task<OrderItem?> CreateAndReturnEntityAsync(OrderItem param, CancellationToken? ct = null)
  {
    if (ct?.IsCancellationRequested == true)
      throw new InvalidOperationException(ct.Value.ToString());

    using (SqlConnection sqlConnection = await _connectionFactory.CreateSqlConnection())
    {
      return await sqlConnection.QuerySingleOrDefaultAsync<OrderItem>("SP_AddOrderItemToCartAndReturnIt", param:
          new
          {
            param.ProductId,
            param.ShoppingCartId,
            param.Quantity,
          }, commandType: System.Data.CommandType.StoredProcedure);
    }
  }

  public async Task<bool> CreateMultipleAsync(List<OrderItem> OrderItems, CancellationToken? ct)
  {
    int NewOrderItemID = 0;
    try
    {

      using (SqlConnection sqlConnection = await _connectionFactory.CreateSqlConnection())
      {
        foreach (OrderItem item in OrderItems)
        {
          if (ct?.IsCancellationRequested == true)
            throw new InvalidOperationException(ct.Value.ToString());

          NewOrderItemID = await sqlConnection.QuerySingleOrDefaultAsync<int>("SP_AddOrderItemToCart", param:
            new
            {
              item.ProductId,
              item.ShoppingCartId,
              item.Quantity
            }, commandType: System.Data.CommandType.StoredProcedure);
        }
      }
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex.Message);
      return false;
    }
    return false;

  }

  public async Task<bool> DeleteAsync(int param, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();
    return await _context.OrderItems.Where(oi => oi.Id == param).ExecuteDeleteAsync() == 1;
  }

  public async Task<List<OrderItem>?> GetAsync(CancellationToken? ct = null)
  {
    return await _context.OrderItems.ToListAsync();
  }
  public async Task<bool> UpdateAsync(OrderItem param, CancellationToken? cancellationToken = null)
  {
    OrderItem? orderItem = _context.OrderItems.Where(oi => oi.Id == param.Id).FirstOrDefault();
    if (orderItem == null) { return false; }

    orderItem = param;
    return await _context.SaveChangesAsync() > 0;
  }

  public async Task<OrderItem?> GetByIDAsync(int ID, CancellationToken? ct)
  {

    ct?.ThrowIfCancellationRequested();
    string sql = @"
  select OrderItems.*, Products.* from OrderItems
    inner join Products on OrderItems.ProductID = Products.ID";


    using (SqlConnection sqlConnection = await _connectionFactory.CreateSqlConnection())
    {
      OrderItem? orderItem = sqlConnection.QueryAsync<OrderItem, Product, OrderItem>(sql, (o, p) =>
      {
        //o.product = p;
        return o;
      }, splitOn: "ID").Result.FirstOrDefault();

      return orderItem;
    }
  }

  public List<OrderItem> GetAsync(int OrderID)
  {
    return _context.OrderItems.Where(i => i.OrderId == OrderID).Include(order => order.Product).ToList();
  }
}
