using System.Data;
using System.Threading;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementation;

public class UserRepo : IUserRepo
{
  SqlConnection _connection;
  EStoreSystemContext _systemContext;
  public UserRepo(IConnectionFactory connectionFactory, EStoreSystemContext eStoreSystemContext)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
    _systemContext = eStoreSystemContext;
  }

  public async Task<List<User>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    //throw new NotImplementedException();
    //if (cancellationToken?.IsCancellationRequested == true)
    //  throw new OperationCanceledException(cancellationToken.Value);

    return (await _connection.QueryAsync<User>("SP_GetAllUseres", commandType: System.Data.CommandType.StoredProcedure)).ToList();
  }

  public async Task<User?> GetByEmail(string email, CancellationToken? ct)
  {
    if (ct?.IsCancellationRequested == true)
      throw new OperationCanceledException(ct.Value);

    return await _connection.QuerySingleOrDefaultAsync<User>("[dbo].[SP_GetUserByEmail]", param: new { email });
  }

  public async Task<User?> GetByIDAsync(Guid ID, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    return await _systemContext.Users.Where(u => u.Id == ID).Include(u => u.DefaultAddress).FirstOrDefaultAsync();
  }

  public async Task<Guid> CreateAsync(User param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.QuerySingleAsync<Guid>("SP_AddNewUser", commandType: CommandType.StoredProcedure, param: new
    {
      EmailAddress = param.EmailAddress,
      Password = param.Password,
    });
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
