using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;
using Serilog;
using Serilog.Core;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;

public abstract class BaseRepository<TEntity> /*: IDataAccess<TEntity> where TEntity : class*/
{
  public IConnectionFactory _connection;
  private SqlConnection _sqlconnection;

  public BaseRepository(IConnectionFactory connection)
  {
    _connection = connection;
    _sqlconnection = _connection.CreateSqlConnection().Result;
  }

  public async Task<int> CreateAsync(string sql, CommandType? commandType, CancellationToken? cancellationToken = default, object? param = null)
  {
    var result = 0;
    
    result = await _sqlconnection.ExecuteAsync(sql, commandType: commandType, param: param);
    
    return result;
  }

  public async Task DeleteAsync(string sql, CommandType? commandType, CancellationToken? cancellationToken = default, object? ID = null)
  {
    try
    {
      await _sqlconnection.ExecuteAsync(sql, param: ID, commandType: commandType);
    }
    catch (OperationCanceledException)
    {
    }
  }

  public async Task<List<T>?> LoadDataAsync<T>(string sql, CommandType? commandType, CancellationToken? cancellationToken = default, object? param = null)
  {
    IEnumerable<T>? results = null;
    try
    {
       results = await _sqlconnection.QueryAsync<T>(sql, param: param, commandType: commandType);
    }
    catch (OperationCanceledException)
    {
      return null;
    }
    return results.ToList();
  }

  public async Task<T?> LoadSingleAsync<T>(string sql, CommandType? commandType, CancellationToken? cancellationToken = default, object? param = null)
  {
    T? results = default;
    try
    {
      results = await _sqlconnection.QuerySingleAsync<T>(sql, param: param, commandType: commandType);
    }
    catch (Exception e)
    {
      Log.Logger.Error(e.Message);
      return results;
    }
    return results;
  }

  public async Task UpdateDataAsync(string sql, CommandType? commandType, CancellationToken? cancellationToken = default, object? param = null)
  {
    try
    {
      var rows = await _sqlconnection.ExecuteAsync(sql, param: param, commandType: commandType);
    }
    catch (OperationCanceledException)
    {
    }

  }
  public async Task<T?> Execute<T>(string sql, CommandType? commandType
                                              ,CancellationToken? cancellationToken, object? param)
  {
    T? result = default;
    try
    {
      result = await _sqlconnection.ExecuteScalarAsync<T>(sql, param: param, commandType: commandType);
      return result;
    }
    catch
    {
      throw;
    }
  }

  public Task<List<TEntity>?> GetAsync(string StoredProc, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public Task<TEntity?> GetSingleAsync(string StoredProc, CancellationToken? cancellationToken = null, object? param = null)
  {
    throw new NotImplementedException();
  }

  public Task<int> CreateAsync(string StoredProc, CancellationToken? cancellationToken = null, object? procedureParameter = null)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(string StoredProc, CancellationToken? cancellationToken = null, object? procedureParameter = null)
  {
    throw new NotImplementedException();
  }

  public Task<bool> DeleteAsync(string StoredProc, CancellationToken? cancellationToken = null, object? Param = null)
  {
    throw new NotImplementedException();
  }
}
