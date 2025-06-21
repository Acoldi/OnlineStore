using System.Data;
using Dapper;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class CustomizationChoiceRepo : ICustomizationChoiceRepo
{
  private readonly IConnectionFactory _connectionFactory;

  public CustomizationChoiceRepo(IConnectionFactory connectionFactory)
  {
    _connectionFactory = connectionFactory;
  }

  public async Task<List<CustomizationChoice>?> GetAsync(CancellationToken? cancellationToken = null)
  {

    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return [.. (await connection.QueryAsync<CustomizationChoice>(
                "SP_GetCustomizationChoices",
                commandType: CommandType.StoredProcedure
            ))];
    }
  }

  public async Task<CustomizationChoice?> GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {

      return await connection.QuerySingleOrDefaultAsync<CustomizationChoice>(
          "SP_GetCustomizationChoiceByID",
          param: new { ID },
          commandType: CommandType.StoredProcedure
      );
    }
  }

  public async Task<int> CreateAsync(CustomizationChoice param, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      int newId = await connection.QuerySingleAsync<int>(
          "SP_AddCustomizationChoice",
          param: param,
          commandType: CommandType.StoredProcedure
      );
      return newId;
    }
  }


  public async Task<bool> UpdateAsync(CustomizationChoice param, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {


      int rowsAffected = await connection.ExecuteAsync(
          "SP_UpdateCustomizationChoice",
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
          "SP_DeleteCustomizationChoice",
          param: new { ID },
          commandType: CommandType.StoredProcedure
      );
      return rowsAffected == 1;
    }
  }
}
