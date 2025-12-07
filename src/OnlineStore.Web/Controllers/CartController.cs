using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.DTOs.Items;
using OnlineStore.Core.Enums;
using OnlineStore.Core.InterfacesAndServices;

namespace OnlineStore.Web.Controllers;
[Route("api/Cart")]
[ApiController]
public class CartController : ControllerBase
{
  private readonly ICartService _cartService;

  public CartController(ICartService cartService)
  {
    _cartService = cartService;
  }

  private Guid GetUserID()
  {
    string? UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (UserID == null)
      throw new AuthenticationException();

    Guid id = new Guid();
    Guid.TryParse(UserID, out id);
    return id;
  }

  /// <summary>
  /// Set a new list of cart items, if there are any current items, they are overriden
  /// </summary>
  /// <param name="CartItems"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  [HttpPost("SetItems")]
  [ProducesResponseType(200)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> SetItems(List<CartItemDto> CartItems, CancellationToken ct = default)
  {
    Guid UserID = GetUserID();

    try
    {
      if (await _cartService.SetCartItemsAsync(UserID, CartItems, ct))
        return Ok();
      else
      {
        Log.Logger.Error("cartService.SetCartItemsAsync failed!!!");
        return BadRequest();
      }
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex, ex.Message);
      return BadRequest();
    }
  }

  [HttpPost("AddItems")]
  [ProducesResponseType(200)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [Authorize]
  public async Task<IActionResult> AddItems(List<CartItemDto> OrderItems, CancellationToken ct)
  {
    Guid UserID = GetUserID();

    try
    {
      await _cartService.SetCartItemsAsync(UserID, OrderItems, ct);
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }

    return Ok();
  }


  [HttpDelete("DeleteItemsFromCart")]
  [ProducesResponseType(200)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> DeleteItemsFromCart(List<int> ItemIDs, CancellationToken ct)
  {
    try
    {
      Guid UserID = GetUserID();

      await _cartService.RemoveItemsFromCartAsync(UserID, ItemIDs, ct);

    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex.Message);
      throw;
    }

    return Ok();
  }

  [HttpPost("PlaceOrder")]
  [ProducesResponseType(200)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [Authorize(Roles = "Admin")]

  public async Task<IActionResult> PlaceOrder(OrderDto OrderDto,
    enPaymentMethod enPaymentMethod, CancellationToken ct)
  {

    try
    {
      Guid userID = GetUserID();

      string? paymentUrl = await _cartService.PlaceOrder(userID, OrderDto, enPaymentMethod, ct);

      if (paymentUrl == null)
      {
        return StatusCode(StatusCodes.Status500InternalServerError);
      }

      return Redirect(paymentUrl);
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }

  }
}
