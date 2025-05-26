using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace OnlineStore.Core.Interfaces.DataAccess;
/// <summary>
/// Interface that defines how Data in a DB is accessed
/// </summary>
public interface IDataAccess
{
  public Task<List<T>?> LoadDataAsync<T>(string StoredProc,CommandType? commandType = default, CancellationToken? cancellationToken = default, 
    object? procedureParameter = null);

  public Task<T?> LoadSingleAsync<T>(string StoredProc,CommandType? commandType, CancellationToken? cancellationToken = default, 
    object? procedureParameter = null);

  public Task<int> CreateAsync(string StoredProc,CommandType? commandType, CancellationToken? cancellationToken = default,
    object? procedureParameter = null);
  
  public Task<int> CreateAsync<T>(string StoredProc,CommandType? commandType, CancellationToken? cancellationToken = default,
    object? procedureParameter = null);

  public Task<bool> UpdateDataAsync(string StoredProc,CommandType? commandType, CancellationToken? cancellationToken = default,
    object? procedureParameter = null);

  public Task<bool> DeleteAsync(string StoredProc,CommandType? commandType, CancellationToken? cancellationToken = default, object? ID = null);

}

