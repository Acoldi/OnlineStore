using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace OnlineStore.Core.Interfaces.DataAccess;
/// <summary>
/// 
/// </summary>
/// <typeparam name="TID">The id type of the entity</typeparam>
public interface IDataAccess<TEntity, TID> where TEntity : class
{
  /// <summary>
  /// Returns a list of T elements
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name=""></param>
  /// <param name="cancellationToken"></param>
  /// <param name="param"></param>
  /// <returns></returns>
  public Task<List<TEntity>?> GetAsync(CancellationToken? cancellationToken = default);

  /// <summary>
  /// Returns a single T
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name=""></param>
  /// <param name="cancellationToken"></param>
  /// <param name="param"></param>
  /// <returns></returns>
  public Task<TEntity?> GetByIDAsync(TID ID, CancellationToken? cancellationToken = default);

  /// <summary>
  /// Returns the newly added ID
  /// </summary>
  /// <param name=""></param>
  /// <param name="cancellationToken"></param>
  /// <param name="param"></param>
  /// <returns></returns>
  public Task<TID> CreateAsync(TEntity param, CancellationToken? cancellationToken = default);
  /// <summary>
  /// 
  /// </summary>
  /// <param name=""></param>
  /// <param name="cancellationToken"></param>
  /// <param name="param"></param>
  /// <returns>How many rows affected</returns>
  public Task<bool> UpdateAsync(TEntity param, CancellationToken? cancellationToken = default);

  public Task<bool> DeleteAsync(TID ID, CancellationToken? cancellationToken = default);

}

