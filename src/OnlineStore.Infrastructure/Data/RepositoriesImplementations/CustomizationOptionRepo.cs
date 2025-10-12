using OnlineStore.Core.InterfacesAndServices.IRepositories;
using System.Data;
using System.Threading;
using Dapper;
using OnlineStore.Core.Entities;
using Microsoft.Data.SqlClient;


namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class CustomizationOptionRepo : ICustomizationOptionRepo
{
  private readonly IConnectionFactory _connectionFactory;

  public CustomizationOptionRepo(IConnectionFactory connectionFactory)
  {
    _connectionFactory = connectionFactory;
  }

  public async Task<List<CustomizationOption>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return [.. (await connection.QueryAsync<CustomizationOption>(
                "SP_GetCustomizationOptions",
                commandType: CommandType.StoredProcedure
            ))];
    }
  }

  public async Task<CustomizationOption?> GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      // Relations using Dapper
      return connection.QueryAsync<CustomizationOption, CustomizationOptionType, CustomizationOption>(
          "SP_GetCustomizationOptionByID", (co, ct) => 
          { 
            co.customizationOptionType = ct;
            return co;
          },
          param: new { ID },
          commandType: CommandType.StoredProcedure,
          splitOn: "ID"
      ).Result.FirstOrDefault();
    }
  }

  public async Task<int> CreateAsync(CustomizationOption param, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      int newId = await connection.QuerySingleAsync<int>(
          "SP_AddCustomizationOption",
          param: param,
          commandType: CommandType.StoredProcedure
      );
      return newId;
    }
  }

  public async Task<bool> UpdateAsync(CustomizationOption param, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      int rowsAffected = await connection.ExecuteAsync(
          "SP_UpdateCustomizationOption",
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
          "SP_DeleteCustomizationOption",
          param: new { ID },
          commandType: CommandType.StoredProcedure
      );
      return rowsAffected == 1;
    }
  }

  public async Task<List<CustomizationOption>?> GetProductCustomizationOptions()
  {
    using (SqlConnection conn = await _connectionFactory.CreateSqlConnection())
    {
      return [.. (await conn.QueryAsync<CustomizationOption>(
                "SP_GetCustomizationOptionsByProductID",
                commandType: CommandType.StoredProcedure
            ))];
    }
  }
}
