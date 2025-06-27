using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.DTOs.Parameters;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.Payment;

namespace OnlineStore.Web.Controllers;
[Route("api/Cart")]
[ApiController]
public class CartController : ControllerBase
{
  private readonly ICartService _cartService;
  private readonly ICartRepo _cartRepo;
  private readonly ICustomerRepo _customerRepo;
  private readonly IPaymentService _paymentService;

  public CartController(ICartRepo cartRepo, ICartService cartService, ICustomerRepo customerRepo, IPaymentService paymentService)
  {
    _cartService = cartService;
    _cartRepo = cartRepo;
    _customerRepo = customerRepo;
    _paymentService = paymentService;
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
  public async Task<IActionResult> AddItems(List<CartItem> cartItems, CancellationToken ct)
  {
    Guid UserID = GetUserID();
    
    try
    {
      await _cartService.SetCartItemsAsync(UserID, cartItems, ct);
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

      int? cartID = await _cartRepo.CreateAsync(UserID);

      await _cartRepo.RemoveCartItemsAsync(cartID.Value);

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
      Order order = new Order(createOrderParam.ShippingAddressID, CustomerID);

      order.ID = await _cartService.PlaceOrder(new Order(createOrderParam.ShippingAddressID, CustomerID), ct);

      string paymentUrl = await _paymentService.GenerateZainCashURL(order);

      return Redirect(paymentUrl);
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }

  }

}
