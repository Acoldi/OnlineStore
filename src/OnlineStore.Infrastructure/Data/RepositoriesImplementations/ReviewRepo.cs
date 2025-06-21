using System.Data;
using Dapper;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class ReviewRepo : IReviewRepo
{
  private readonly IConnectionFactory _connectionFactory;

  public ReviewRepo(IConnectionFactory connectionFactory)
  {
    _connectionFactory = connectionFactory;
  }

  public async Task<List<Review>?> GetAsync(CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return (await connection.QueryAsync<Review>(
          "SP_GetReviews", // Assuming SP_GetReviews retrieves all reviews
          commandType: CommandType.StoredProcedure
      )).AsList(); // AsList() is often used with Dapper to ensure it's a List
    }
  }

  public async Task<Review?> GetByIDAsync(int ID, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      return await connection.QuerySingleOrDefaultAsync<Review>(
          "SP_GetReviewByID", // Assuming SP_GetReviewByID retrieves by ID
          param: new { ID },
          commandType: CommandType.StoredProcedure
      );
    }
  }

  public async Task<int> CreateAsync(Review param, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      int newId = await connection.QuerySingleAsync<int>(
          "SP_AddReview", // Assuming SP_AddReview inserts a new review
          param: param, // Dapper will map properties directly to SP parameters
          commandType: CommandType.StoredProcedure
      );
      return newId;
    }
  }

  public async Task<bool> UpdateAsync(Review param, CancellationToken? cancellationToken = null)
  {
    cancellationToken?.ThrowIfCancellationRequested();

    using (IDbConnection connection = await _connectionFactory.CreateSqlConnection())
    {
      int rowsAffected = await connection.ExecuteAsync(
          "SP_UpdateReview", // Assuming SP_UpdateReview updates an existing review
          param: param, // Dapper will map properties directly to SP parameters
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
          "SP_DeleteReview", // Assuming SP_DeleteReview deletes a review
          param: new { ID },
          commandType: CommandType.StoredProcedure
      );
      return rowsAffected == 1;
    }
  }
}
