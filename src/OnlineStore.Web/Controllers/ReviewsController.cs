using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OnlineStore.Core.DTOs.Parameters;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Web.Controllers;
[Route("api/Reviews")]
[ApiController]
public class ReviewsController : ControllerBase
{
  private readonly IReviewRepo _reviewRepo;
  public ReviewsController(IReviewRepo reviewRepo)
  {
    _reviewRepo = reviewRepo;
  }

  [Authorize]
  [HttpGet("{productId}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetReview(int reviewID, CancellationToken cancellationToken)
  {
    Review? reviews = await _reviewRepo.GetByIDAsync(reviewID, cancellationToken);

    if (reviews == null)
    {
      return NotFound();
    }
    return Ok(reviews);
  }

  [Authorize]
  [HttpGet(Name = "GetReviews")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetReviews(CancellationToken cancellationToken)
  {
    List<Review>? reviews = await _reviewRepo.GetAsync(cancellationToken);

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
  public async Task<IActionResult> AddReview(Review review, CancellationToken cancellationToken)
  {
    if (review == null)
    {
      return BadRequest("Invalid review data.");
    }
    int newId = await _reviewRepo.CreateAsync(review, cancellationToken);
    return CreatedAtAction(nameof(GetReview), new { ReviewID = review.ID }, newId);
    //return Created();
  }

  [Authorize]
  [HttpDelete("Delete/{ID}", Name = "Delete")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Delte(int ID, CancellationToken cancellationToken)
  {
    if (!await _reviewRepo.DeleteAsync(ID, cancellationToken))
    {
      return BadRequest("Review doesn't exist");
    }
    return NoContent();
    //return Created();
  }

  [Authorize]
  [HttpPut("Update/", Name = "Update")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Update(Review review, CancellationToken cancellationToken)
  {
    if (!await _reviewRepo.UpdateAsync(review, cancellationToken))
    {
      return BadRequest("Review doesn't exist");
    }
    return NoContent();
    //return Created();
  }


  //[HttpGet("Product")]
  //[]
  //public Task<IActionResult> GetProductReviews(int productID, CancellationToken)
  //{
  //  _reviewRepo.getprod
  //}
}
