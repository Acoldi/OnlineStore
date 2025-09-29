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
  SqlConnection _connection;
  public OrderRepo(IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
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

    return await _connection.QuerySingleAsync<Order>("SP_GetOrderByID", commandType: CommandType.StoredProcedure, param: new { ID = param });
  }

  public async Task<Order> GetOrderWithRelatedCategories(int OrderID)
  {
    Order? order = await GetByIDAsync(OrderID);

    if (order != null)
    {
      order.RelatedCategories = await ItemsCategories(OrderID);
      return order;
    }
    else
      throw new InvalidOperationException("No Order with ID: " + OrderID);
  }


  /// <summary>
  /// Total amount value is added on the DB level
  /// </summary>
  /// <param name="order"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="OperationCanceledException"></exception>
  public async Task<int> CreateAsync(Order order, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.QuerySingleOrDefault("SP_AddOrder", commandType: System.Data.CommandType.StoredProcedure, 
      param: new { order.CustomerID, order.ShippingAddressID, order.OrderStatus });
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

    return await _connection.ExecuteAsync("SP_DeleteOrder", new { ID = param }, null, null, System.Data.CommandType.StoredProcedure) == 1;
  }

  public async Task<List<string>> ItemsCategories(int OrderID)
  {
    return [.. await _connection.QueryAsync<string>("SP_GetOrderItemsCategories", commandType: CommandType
      .StoredProcedure, param: OrderID)];
  }

}
