using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using Serilog;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class OrderItemRepo : IOrderItemRepo
{
  SqlConnection _connection;
  public OrderItemRepo(IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
  }

  public async Task<int> CreateAsync(OrderItem param, CancellationToken? ct = null)
  {
    if (ct?.IsCancellationRequested == true)
      throw new InvalidOperationException(ct.Value.ToString());

    return await _connection.ExecuteAsync("SP_AddOrderItemToCart", param: param, commandType: System.Data.CommandType.StoredProcedure);
  }

  public async Task<bool> CreateMultipleAsync(List<OrderItem> OrderItems, CancellationToken? ct)
  {

    int NewOrderItemID = 0;
    try
    {

      foreach (OrderItem item in OrderItems)
      {
        if (ct?.IsCancellationRequested == true)
          throw new InvalidOperationException(ct.Value.ToString());

        NewOrderItemID = await _connection.QuerySingleAsync<int>("SP_AddOrderItemToCart", param:
          new
          {
            item.ProductID,
            item.ShoppingCartID,
            item.Quantity
          }, commandType: System.Data.CommandType.StoredProcedure);
      }
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex.Message);
      return false;
    }
    return false;

  }

  public Task<bool> DeleteAsync(int param, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public Task<List<OrderItem>?> GetAsync(CancellationToken? ct = null)
  {
    throw new NotImplementedException();
    //if (ct?.IsCancellationRequested == true)
    //  throw new InvalidOperationException(ct.Value.ToString());

  }

  public Task<bool> UpdateAsync(OrderItem param, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  Task<OrderItem?> IDataAccess<OrderItem, int>.GetByIDAsync(int ID, CancellationToken? cancellationToken)
  {
    throw new NotImplementedException();
  }
}
