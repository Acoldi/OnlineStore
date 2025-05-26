using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Infrastructure.Data;

public class DataAccessService : IDataAccess
{
  public IConnectionFactory _connection;
  private SqlConnection _sqlconnection;

  public DataAccessService(IConnectionFactory connection)
  {
    _connection = connection;
    _sqlconnection = _connection.CreateSqlConnection().Result;
  }

  public async Task<int> CreateAsync(string sql, CommandType? commandType, CancellationToken? cancellationToken = default, object? param = null)
  {
    int result = 0;
    
    result = await _sqlconnection.ExecuteAsync(sql, commandType: commandType, param: param);
    
    return result;
  }
  public async Task<int> CreateAsync<T>(string sql, CommandType? commandType, CancellationToken? cancellationToken = default,
  object? param = null)
  {

    int result = 0;

    result = await _sqlconnection.QuerySingleAsync<int>(sql, commandType: commandType, param: param);

    return result;
  }
  public async Task<int> CreateMultipleAsync<T>(string sql, CommandType? commandType, CancellationToken? cancellationToken = default,
  object? param = null)
  {

    int result = 0;

    result = await _sqlconnection.QuerySingleAsync<int>(sql, commandType: commandType, param: param);

    return result;
  }

  public async Task<bool> DeleteAsync(string sql, CommandType? commandType, CancellationToken? cancellationToken = default, object? ID = null)
  {
    try
    {
      int Rows = await _sqlconnection.ExecuteAsync(sql, param: new { ID = ID}, commandType: commandType);
      return (Rows > 0);
    }
    catch (OperationCanceledException)
    {
      return false;
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
    IEnumerable<T>? results = null;
    try
    {
      results = await _sqlconnection.QueryAsync<T>(sql, param: param, commandType: commandType);
    }
    catch (OperationCanceledException)
    {
      return default;
    }
    return results.FirstOrDefault();
  }

  public async Task<bool> UpdateDataAsync(string sql, CommandType? commandType, CancellationToken? cancellationToken = default, object? param = null)
  {
    try
    {
      int rows = await _sqlconnection.ExecuteAsync(sql, param: param, commandType: commandType);
      return rows > 0;
    }
    catch (OperationCanceledException)
    {
      return false; 
    }

  }
}
