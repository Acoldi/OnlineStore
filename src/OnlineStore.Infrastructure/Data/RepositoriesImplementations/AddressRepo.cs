using System.Data;
using System.Threading;
using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Web.RepositoriesImplementations;

public class AddressRepo : IAddressRepo
{
  SqlConnection _connection;
  public AddressRepo(IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
  }

  public async Task<List<Address>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return (await _connection.QueryAsync<Address>("SP_GetAllAddresses", commandType: System.Data.CommandType.StoredProcedure)).ToList();
  }

  public async Task<Address?> GetByIDAsync(int param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.QuerySingleAsync<Address>("SP_GetAddressByID", commandType: CommandType.StoredProcedure, param: new { ID = param});
  }

  public async Task<int> CreateAsync(Address param, CancellationToken ? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.QuerySingleOrDefault("SP_AddAddress", commandType: System.Data.CommandType.StoredProcedure, param: param);
  }

  public async Task<bool> UpdateAsync(Address param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.ExecuteAsync("SP_UpdateAddress", new { updatedAddress = param }, null, null, System.Data.CommandType.StoredProcedure) == 1;
  }

  public async Task<bool> DeleteAsync(int param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.ExecuteAsync("SP_DeleteAddress", new { ID = param}, null, null, System.Data.CommandType.StoredProcedure) == 1;
  }

}
