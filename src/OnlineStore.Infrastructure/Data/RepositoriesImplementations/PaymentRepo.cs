using System.Data;
using System.Threading;
using Dapper;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class PaymentRepo : IPaymentRepo
{
  private readonly IConnectionFactory _connectionFactory;

  public PaymentRepo(IConnectionFactory connectionFactory)
  {
    _connectionFactory = connectionFactory;
  }

  public async Task<List<Payment>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return (await connection.QueryAsync<Payment>(
          "SP_GetPayments",
          commandType: CommandType.StoredProcedure
      )).AsList();
    }
  }

  public async Task<Payment?> GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return await connection.QuerySingleOrDefaultAsync<Payment>(
          "SP_GetPaymentByID",
          param: new { ID },
          commandType: CommandType.StoredProcedure
      );
    }
  }

  public async Task<int> CreateAsync(Payment payment, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      int newId = await connection.QuerySingleAsync<int>(
          "SP_AddPayment", 
          param: payment,
          commandType: CommandType.StoredProcedure
      );
      return newId;
    }
  }

  public async Task<bool> UpdateAsync(Payment param, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      int rowsAffected = await connection.ExecuteAsync(
          "SP_UpdatePayment",
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
          "SP_DeletePayment",
          param: new { ID },
          commandType: CommandType.StoredProcedure
      );
      return rowsAffected == 1;
    }
  }

  public async Task<Payment> GetByTransactionID(string transactionID, CancellationToken? ct = null)
  {
        ct?.ThrowIfCancellationRequested();

        using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
        {
            Payment result = await connection.QuerySingleAsync<Payment>(
                "SP_DeletePayment",
                param: new { transactionID },
                commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }
}
