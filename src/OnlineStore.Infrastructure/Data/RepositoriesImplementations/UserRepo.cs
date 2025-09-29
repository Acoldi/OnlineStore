using System.Data;
using System.Threading;
using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementation;

public class UserRepo : IUserRepo
{
  SqlConnection _connection;
  public UserRepo(IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
  }

  public Task<List<User>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
    //if (cancellationToken?.IsCancellationRequested == true)
    //  throw new OperationCanceledException(cancellationToken.Value);

    //return (await _connection.QueryAsync<User>("SP_GetAllUseres", commandType: System.Data.CommandType.StoredProcedure)).ToList();
  }

  public async Task<User?> GetByEmail(string email, CancellationToken? ct)
  {
    if (ct?.IsCancellationRequested == true)
      throw new OperationCanceledException(ct.Value);

    return await _connection.QuerySingleAsync<User>("[dbo].[SP_GetUserByEmail]", param: new { email });
  }

  public async Task<User?> GetByIDAsync(Guid ID, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    return await _connection.QuerySingleAsync<User>("[SP_GetUserByID]", ID, commandType: CommandType.StoredProcedure);
  }

  public async Task<Guid> CreateAsync(User param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.QuerySingleOrDefault("SP_AddNewUser", commandType: System.Data.CommandType.StoredProcedure, param: param);
  }

  public Task<bool> UpdateAsync(User param, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public Task<bool> DeleteAsync(Guid ID, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }
}
