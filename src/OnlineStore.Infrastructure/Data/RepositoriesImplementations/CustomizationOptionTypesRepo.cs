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
public class CustomizationOptionTypesRepo : ICustomizationOptionTypesRepo
{
  SqlConnection _connection;
  public CustomizationOptionTypesRepo(IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
  }

  public async Task<List<CustomizationOptionType>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return (await _connection.QueryAsync<CustomizationOptionType>("[SP_GetAllCustomizationOptionTypes]", commandType: System.Data.CommandType.StoredProcedure)).ToList();
  }

  public async Task<CustomizationOptionType?> GetByIDAsync(int param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.QuerySingleOrDefaultAsync<CustomizationOptionType>("SP_GetCustomizationOptionTypeByID", commandType: CommandType.StoredProcedure, param: new { ID = param });
  }

  public async Task<int> CreateAsync(CustomizationOptionType param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.QuerySingleOrDefault("SP_AddCustomizationOptionType", commandType: System.Data.CommandType.StoredProcedure, param: param);
  }

  public async Task<bool> UpdateAsync(CustomizationOptionType param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.ExecuteAsync("[SP_UpdateCustomizationOptionType]", new { updatedCustomizationOptionType = param }, null, null, System.Data.CommandType.StoredProcedure) == 1;
  }

  public async Task<bool> DeleteAsync(int param, CancellationToken? cancellationToken = null)
  {
    if (cancellationToken?.IsCancellationRequested == true)
      throw new OperationCanceledException(cancellationToken.Value);

    return await _connection.ExecuteAsync("[SP_DeleteCustomizationOptionType]", new { ID = param }, null, null, System.Data.CommandType.StoredProcedure) == 1;
  }
}
