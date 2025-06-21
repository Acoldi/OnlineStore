using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class InventoryRepo : IInventoryRepo
{
  SqlConnection _connection;
  public InventoryRepo(IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
  }

  public async Task<int> CreateAsync(Inventory param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.QuerySingleAsync("SP_AddInventory", commandType: System.Data.CommandType.StoredProcedure,
      param: param);
  }

  public async Task<bool> DeleteAsync(int ID, CancellationToken? cancellationToken = null)
  {
    return await _connection.ExecuteAsync("SP_DeleteInventory", commandType: System.Data.CommandType.StoredProcedure,
      param: new { ID }) == 1;
  }

  public async Task<List<Inventory>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return (await _connection.QueryAsync<Inventory>("SP_GetInventories", commandType: System.Data.CommandType.StoredProcedure)).ToList();
  }

  public async Task<Inventory?> GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    return await _connection.QuerySingleAsync<Inventory>("SP_GetInventories", commandType: System.Data.CommandType.StoredProcedure,
      param: new {ID});
  }

  public async Task<bool> UpdateAsync(Inventory param, CancellationToken? cancellationToken = null)
  {
    return await _connection.ExecuteAsync("SP_UpdateInventory", commandType: System.Data.CommandType.StoredProcedure,
      param: param) == 1;
  }
}
