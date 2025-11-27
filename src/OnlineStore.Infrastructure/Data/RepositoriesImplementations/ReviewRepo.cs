using System.Data;
using Dapper;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.Mappers;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Infrastructure.Data.RepositoriesImplementations;
public class ReviewRepo : IReviewRepo
{
  private readonly IConnectionFactory _connectionFactory;
  private readonly EStoreSystemContext _eStoreSystemContext;

  public ReviewRepo(IConnectionFactory connectionFactory, EStoreSystemContext eStoreSystemContext)
  {
    _connectionFactory = connectionFactory;
    _eStoreSystemContext = eStoreSystemContext;
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
      int newId = await connection.QuerySingleOrDefaultAsync<int>(
          "SP_AddReview",
          param: new
          {
            param.ProductId,
            param.UserId,
            param.Rating,
            param.Comment,
            param.CreatedAt,
            param.IsApproved,
          },
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
          "SP_UpdateReview",
          param: new
          {
            param.Id,
            param.ProductId,
            param.UserId,
            param.Rating,
            param.Comment,
            param.CreatedAt,
            param.IsApproved,
          },
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

  public Task<List<Review>> GetAllAcceptedReviews()
  {
    return Task.FromResult(_eStoreSystemContext.Reviews.Where(r => r.IsApproved == true).ToList());
  }

  public Task<List<Review>> GetAllAcceptedReviews(int productID)
  {
    return Task.FromResult(_eStoreSystemContext.Reviews.Where(r => r.IsApproved == true && r.ProductId == productID).ToList());
  }


}
