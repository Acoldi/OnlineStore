using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.InterfacesAndServices.Reviews;

public interface IReviewService
{
  Task<List<ReviewDto>?> GetAsync(CancellationToken cancellationToken = default);
  Task<List<ReviewDto>?> GetAcceptedReviewsAsync(CancellationToken cancellationToken = default);
  Task<List<ReviewDto>?> GetAcceptedReviesAsyncByProductID(int productID, CancellationToken cancellationToken = default);
  Task<ReviewDto?> GetByIDAsync(int ID, CancellationToken cancellationToken = default);
  Task<int> CreateAsync(ReviewDto param, CancellationToken cancellationToken = default);
  Task<bool> UpdateAsync(ReviewDto param, CancellationToken cancellationToken = default);
  Task<bool> DeleteAsync(int ID, CancellationToken cancellationToken = default);
}
