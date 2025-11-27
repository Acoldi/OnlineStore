using System.Reflection.Metadata.Ecma335;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.Mappers;

namespace OnlineStore.Core.InterfacesAndServices.Reviews;

public class ReviewService : IReviewService
{
  private readonly IReviewRepo _reviewRepo;

  public ReviewService(IReviewRepo reviewRepo)
  {
    _reviewRepo = reviewRepo;
  }

  public async Task<List<ReviewDto>?> GetAsync(CancellationToken cancellationToken = default)
  {
    List<Review>? reviews = await _reviewRepo.GetAsync(cancellationToken);

    if (reviews == null) return null;

    return [.. reviews.Select(r => ReviewMapper.toDto(r))];
  }

  public async Task<ReviewDto?> GetByIDAsync(int ID, CancellationToken cancellationToken = default)
  {
    Review? review = await _reviewRepo.GetByIDAsync(ID);

    if (review == null) return null;

    return ReviewMapper.toDto(review);
  }

  public async Task<int> CreateAsync(ReviewDto param, CancellationToken cancellationToken = default)
  {
    return await _reviewRepo.CreateAsync(ReviewMapper.toEntity(param), cancellationToken);
  }

  public async Task<bool> UpdateAsync(ReviewDto param, CancellationToken cancellationToken = default)
  {
    return await _reviewRepo.UpdateAsync(ReviewMapper.toEntity(param), cancellationToken);
  }

  public async Task<bool> DeleteAsync(int ID, CancellationToken cancellationToken = default)
  {
    return await _reviewRepo.DeleteAsync(ID, cancellationToken);
  }

  public async Task<List<ReviewDto>?> GetAcceptedReviewsAsync(CancellationToken cancellationToken = default)
  {
    List<Review> reviews = await _reviewRepo.GetAllAcceptedReviews();

    return reviews.Select(r => ReviewMapper.toDto(r)).ToList();
  }

  public async Task<List<ReviewDto>?> GetAcceptedReviesAsyncByProductID(int productID, CancellationToken cancellationToken = default)
  {
    List<Review> reviews = await _reviewRepo.GetAllAcceptedReviews(productID);

    return reviews.Select(r => ReviewMapper.toDto(r)).ToList();
  }
}
