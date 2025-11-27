using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.Reviews;

namespace OnlineStore.Web.Controllers;
[Route("api/Reviews")]
[ApiController]
public class ReviewsController : ControllerBase
{
  private readonly IReviewService _reviewService;
  public ReviewsController(IReviewService reviewService)
  {
    _reviewService = reviewService;
  }

  [Authorize]
  [HttpGet("{reviewID}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetReview(int reviewID, CancellationToken cancellationToken)
  {
    ReviewDto? reviews = await _reviewService.GetByIDAsync(reviewID, cancellationToken);

    if (reviews == null)
    {
      return NotFound();
    }
    return Ok(reviews);
  }

  [HttpGet(Name = "GetReviews")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [Authorize]
  public async Task<IActionResult> GetReviews(CancellationToken cancellationToken)
  {
    List<ReviewDto>? reviews = await _reviewService.GetAcceptedReviewsAsync(cancellationToken);

    if (reviews == null)
    {
      return NotFound("No reviews found for this product.");
    }
    return Ok(reviews);
  }


  [Authorize]
  [HttpPost]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> AddReview(ReviewDto review, CancellationToken cancellationToken)
  {
    if (review == null)
    {
      return BadRequest("Invalid review data.");
    }
    int newId = await _reviewService.CreateAsync(review, cancellationToken);
    return CreatedAtAction(nameof(GetReview), review);
  }

  [Authorize]
  [HttpDelete("{ID}", Name = "Delete")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Delte(int ID, CancellationToken cancellationToken)
  {
    if (!await _reviewService.DeleteAsync(ID, cancellationToken))
    {
      return BadRequest("Review doesn't exist");
    }
    return NoContent();
  }

  [Authorize]
  [HttpPut("Update/", Name = "Update")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Update(ReviewDto review, CancellationToken cancellationToken)
  {
    if (!await _reviewService.UpdateAsync(review, cancellationToken))
    {
      return BadRequest("Review doesn't exist");
    }
    return NoContent();
  }

}
