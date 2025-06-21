using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.DTOs.Parameters;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Infrastructure.Data.RepositoriesImplementations;

namespace OnlineStore.Web.Controllers;
[Route("api/Cart")]
[ApiController]
public class CartController : ControllerBase
{
  private readonly ICartService _cartService;
  private readonly ICartRepo _cartRepo;
  private readonly ICustomerRepo _customerRepo;

  public CartController(ICartRepo cartRepo, ICartService cartService, ICustomerRepo customerRepo)
  {
    _cartService = cartService;
    _cartService = cartService;
    _cartRepo = cartRepo;
    _customerRepo = customerRepo;
  }

  private Guid GetUserID()
  {
    string? UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (UserID == null)
      throw new Exception("Tampered with JWT!");

      return new Guid(UserID);
  }

  [HttpPost("AddItems")]
  [ProducesResponseType(200)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(Roles = "Admin")]
  public async Task<IActionResult> AddItems(List<Tuple<CartItem, CustomizedItem?>> cartItems, CancellationToken ct)
  {
    Guid? UserID = GetUserID();
    if (UserID == null)
      return Unauthorized("Did not find userID!");

    try
    {
      await _cartService.SetItemsAsync(UserID.Value, cartItems, ct);
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
  public async Task<IActionResult> DeleteItemsFromCart(CancellationToken ct)
  {
    try
    {
      Guid UserID = GetUserID();

      int cartID = await _cartRepo.CreateAsync(UserID);

      await _cartRepo.RemoveCartItemsAsync(cartID);
      
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
  public async Task<IActionResult> PlaceOrder(CreateOrderParam createOrderParam, CancellationToken ct)
  {
    int CustomerID = await _customerRepo.CreateAsync(new Customer(GetUserID()), ct);

    try
    {
      await _cartService.PlaceOrder(new Order(createOrderParam.ShippingAddressID, CustomerID), ct);
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }

    return Ok();
  }

}
