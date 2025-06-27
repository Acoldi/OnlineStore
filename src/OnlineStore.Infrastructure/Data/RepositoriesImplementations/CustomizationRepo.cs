using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class CustomizationRepo : ICustomizationRepo
{
  private readonly IConnectionFactory _connectionFactory;

  public CustomizationRepo(IConnectionFactory connectionFactory)
  {
    _connectionFactory = connectionFactory;
  }

  public async Task<List<Customization>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return [.. (await connection.QueryAsync<Customization>(
                "SP_GetCustomizations",
                commandType: CommandType.StoredProcedure
            ))];
    }
  }

  /// <summary>
  /// After inserting a new customizatin, the item price is updated on the DB level
  /// </summary>
  /// <param name="ID"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public async Task<Customization?> GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return await connection.QuerySingleOrDefaultAsync<Customization>(
          "SP_GetCustomizationByID",
          param: new { ID },
          commandType: CommandType.StoredProcedure
      );
    }
  }

  /// <summary>
  /// After insertion, Addtional cost is added to the item on the DB level
  /// </summary>
  /// <param name="param"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public async Task<int> CreateAsync(Customization param, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      int newId = await connection.QuerySingleAsync<int>(
          "SP_AddCustomization",
          param: param, 
          commandType: CommandType.StoredProcedure
      );
      return newId;
    }
  }

  public async Task<bool> UpdateAsync(Customization param, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      int rowsAffected = await connection.ExecuteAsync(
          "SP_UpdateCustomization",
          param: param,
          commandType: CommandType.StoredProcedure
      );
      return rowsAffected == 1;
    }
  }

  public async Task<bool> DeleteAsync(int ID, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      int rowsAffected = await connection.ExecuteAsync(
          "SP_DeleteCustomization",
          param: new { ID },
          commandType: CommandType.StoredProcedure
      );
      return rowsAffected == 1;
    }
  }

  
}
