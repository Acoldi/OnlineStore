using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class OrderRepo : IOrderRepo
{
  private IConnectionFactory _connectionFactory;
  public OrderRepo(IConnectionFactory connectionFactory)
  {
    _connectionFactory = connectionFactory;
  }

  public Task<List<Order>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();

    //if (cancellationToken?.IsCancellationRequested == true)
    //  throw new OperationCanceledException(cancellationToken.Value);

    //return (await _connection.QueryAsync<Order>("SP_GetAllOrderes", commandType: CommandType.StoredProcedure)).ToList();
  }

  public async Task<Order?> GetByIDAsync(int param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    using (SqlConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return await connection.QuerySingleOrDefaultAsync<Order>("SP_GetOrderByID", commandType: CommandType.StoredProcedure, param: new { ID = param });
    }
  }

  public async Task<List<string>> GeCategoriesRelatedToOrder(int OrderID)
  {
    Order? order = await GetByIDAsync(OrderID);

    if (order != null)
    {
      return await ItemsCategories(OrderID);
    }
    else
      throw new InvalidOperationException("No Order with ID: " + OrderID);
  }


  /// <remarks>
  ///   Total amount value is added on the DB level. Items are related to this order instead of the cart.
  /// </remarks>
  /// <param name="order"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="OperationCanceledException"></exception>
  public async Task<int> CreateAsync(Order order, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (SqlConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return await connection.QuerySingleOrDefault("SP_AddOrder", commandType: CommandType.StoredProcedure,
      param: new { order.CustomerId, order.ShippingAddressId, order.OrderStatus });
    }
  }

  public Task<bool> UpdateAsync(Order order, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();

    //if (cancellationToken?.IsCancellationRequested == true)
    //  throw new OperationCanceledException(cancellationToken.Value);

    //return await _connection.ExecuteAsync("SP_UpdateOrder", new { updatedOrder = param }, null, null, System.Data.CommandType.StoredProcedure) == 1;
  }

  public async Task<bool> DeleteAsync(int param, CancellationToken? cancellationToken = null)
  {
    //throw new NotImplementedException();

    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);
    using (SqlConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return await connection.ExecuteAsync("SP_DeleteOrder", new { ID = param }, null, null, System.Data.CommandType.StoredProcedure) == 1;
    }
  }

  private async Task<List<string>> ItemsCategories(int OrderID)
  {
    using (SqlConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return [.. await connection.QueryAsync<string>("SP_GetOrderItemsCategories", commandType: CommandType
      .StoredProcedure, param: OrderID)];
    }
  }

}
