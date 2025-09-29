using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.DTOs.Parameters;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
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
  private readonly IZainCashPaymentService _ZainCashpaymentService;
  private readonly IPaytabPaymentService _PayTabpaymentService;
  private readonly IAddressRepo _AddressRepo;
  private readonly ICityRepo _cityRepo;
  private readonly IUserRepo _UserRepo;

  public CartController(IUserRepo userRepo, ICityRepo cityRepo, IAddressRepo address, IPaytabPaymentService paytabPaymentService,
    ICartRepo cartRepo, ICartService cartService, ICustomerRepo customerRepo,
    IZainCashPaymentService paymentService)
  {
    _UserRepo = userRepo;
    _cityRepo = cityRepo;
    _cartService = cartService;
    _cartRepo = cartRepo;
    _customerRepo = customerRepo;
    _ZainCashpaymentService = paymentService;
    _PayTabpaymentService = paytabPaymentService;
    _AddressRepo = address;
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
  public async Task<IActionResult> PlaceOrder(CreateOrderParam createOrderParam, 
    enPaymentMethod enPaymentMethod, CancellationToken ct)
  {

    try
    {
      Guid userID = GetUserID();
      int CustomerID = await _customerRepo.CreateAsync(new Customer(userID), ct);
      Order order = new Order(createOrderParam.ShippingAddressID, CustomerID);

      order.ID = await _cartService.PlaceOrder(new Order(createOrderParam.ShippingAddressID, CustomerID), ct);
      User? user = await _UserRepo.GetByIDAsync(userID);

      string paymentUrl = "";
      switch (enPaymentMethod)
      {
        case enPaymentMethod.MasterVisa:

          Address? address = await _AddressRepo.GetByIDAsync(order.ShippingAddressID);

          if (address == null) return BadRequest("Shipping Address is not provided");
          if (user!.PhoneNumber == null) return BadRequest("Phone number is not provided");

          paymentUrl = await _PayTabpaymentService.GenereateTransactionURL(order,
            new Core.ValueObjects.CustomerDetails()
            {
              city = address.CityName!,
              country = address.CountryName!,
              email = user!.EmailAddress,
              name = user.FirstName + " " + user.LastName,
              phone = user.PhoneNumber,
              state = address.StateName!,
              street1 = "" // ToDo: check with PayTabs if this is accepted
              ,zip = "10011"
            });

          break;
        case enPaymentMethod.ZainCash:
          paymentUrl = await _ZainCashpaymentService.GenerateZainCashPaymentURL(order);
          break;
        case enPaymentMethod.Other:
          break;
        default:
          break;
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
