using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class CustomerRepo : ICustomerRepo
{
  SqlConnection _connection;
  public CustomerRepo(IConnectionFactory connectionFactory)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
  }

  public async Task<int> CreateAsync(Customer param, CancellationToken? cancellationToken = null)
  {
    return await _connection.QuerySingleAsync("SP_AddCustomer", commandType: System.Data.CommandType.StoredProcedure,
      param: param);
  }

  public Task<bool> DeleteAsync(int ID, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public Task<List<Customer>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Customer param, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }

  public Task<Customer?> GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    throw new NotImplementedException();
  }
}
