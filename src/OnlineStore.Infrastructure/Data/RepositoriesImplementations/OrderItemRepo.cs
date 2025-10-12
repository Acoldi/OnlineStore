using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using Serilog;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class OrderItemRepo : IOrderItemRepo
{
  IConnectionFactory _connectionFactory;
  IProductRepo _productRepo;
  public OrderItemRepo(IConnectionFactory connectionFactory, IProductRepo productrepo)
  {
    _connectionFactory = connectionFactory;
    _productRepo = productrepo;
  }

 
  public async Task<int> CreateAsync(OrderItem param, CancellationToken? ct = null)
  {
    if (ct?.IsCancellationRequested == true)
      throw new InvalidOperationException(ct.Value.ToString());

    using (SqlConnection sqlConnection = await _connectionFactory.CreateSqlConnection())
    {
      // - [X] This procedure should add the price automatically
      return await sqlConnection.QuerySingleAsync("SP_AddOrderItemToCart", param: param, commandType: System.Data.CommandType.StoredProcedure);
    }
  }
 
  public async Task<OrderItem> CreateAndReturnEntityAsync(OrderItem param, CancellationToken? ct = null)
  {
    if (ct?.IsCancellationRequested == true)
      throw new InvalidOperationException(ct.Value.ToString());

    using (SqlConnection sqlConnection = await _connectionFactory.CreateSqlConnection())
    {
      return await sqlConnection.QuerySingleAsync("SP_AddOrderItemToCartAndReturnIt", param: param, commandType: System.Data.CommandType.StoredProcedure);
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

          NewOrderItemID = await sqlConnection.QuerySingleAsync<int>("SP_AddOrderItemToCart", param:
            new
            {
              item.ProductID,
              item.ShoppingCartID,
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

  public Task<bool> DeleteAsync(int param, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public Task<List<OrderItem>?> GetAsync(CancellationToken? ct = null)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(OrderItem param, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
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
        o.product = p;
        return o;
      }, splitOn: "ID").Result.FirstOrDefault();

      return orderItem;
    }
  }
}
