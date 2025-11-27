using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class CustomerRepo : ICustomerRepo
{
  private readonly SqlConnection _connection;
  private readonly EStoreSystemContext _context;
  public CustomerRepo(IConnectionFactory connectionFactory, EStoreSystemContext eStoreSystemContext)
  {
    _connection = connectionFactory.CreateSqlConnection().Result;
    _context = eStoreSystemContext;
  }

  public async Task<int> CreateAsync(Customer param, CancellationToken? cancellationToken = null)
  {
    return await _connection.QuerySingleOrDefaultAsync("SP_AddCustomer", commandType: System.Data.CommandType.StoredProcedure,
      param: param);
  }

  // Shifting from Dapper to EF (2025-11-11 1:06)
  public async Task<bool> DeleteAsync(int ID, CancellationToken? cancellationToken = null)
  {
    return await _context.Customers.Where(c => c.Id == ID).ExecuteDeleteAsync() > 0;
  }

  public async Task<List<Customer>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    return await _context.Customers.ToListAsync();
  }

  public bool UpdateAsync(Customer param, CancellationToken? cancellationToken = null)
  {
    Customer? customer = _context.Customers.Where(c => c.Id == param.Id).FirstOrDefault();

    if (customer != null)
    {
      customer = param;

      return _context.SaveChanges() > 0;
    }
    return false;
  }

  public Customer? GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    return _context.Customers.Where(c => c.Id == ID).FirstOrDefault();
  }
}
