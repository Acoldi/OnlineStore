using OnlineStore.Core.DTOs;
using OnlineStore.Core.DTOs.Items;
using OnlineStore.Core.DTOs.PaytabsDTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.Exceptions;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.PaymentServices;
using OnlineStore.Core.Mappers;
using OnlineStore.Core.Mappers.ItemMappers;
using OnlineStore.Core.Values;
using Serilog;

namespace OnlineStore.Core.InterfacesAndServices;
public class CartService : ICartService
{
  private readonly IOrderRepo _orderRepo;
  private readonly IOrderItemRepo _orderItemRepo;
  private readonly ICartRepo _cartRepo;
  private readonly ICustomerRepo _customerRepo;
  private readonly IAddressRepo _addressRepo;
  private readonly IPaymentService _paytabPaymentService;
  private readonly IZainCashPaymentService _zainCashPaymentService;
  private readonly IUserRepo _userRepo;

  public CartService(
      ICartRepo cartRepo,
      IOrderRepo orderRepo,
      IOrderItemRepo orderItem,
      ICustomerRepo customerRepo,
      IAddressRepo addressRepo,
      IPaymentService paytabPaymentService,
      IUserRepo userRepo,
      IZainCashPaymentService zainCashPaymentService)
  {
    _orderRepo = orderRepo;
    _orderItemRepo = orderItem;
    _cartRepo = cartRepo;
    _customerRepo = customerRepo;
    _addressRepo = addressRepo;
    _paytabPaymentService = paytabPaymentService;
    _userRepo = userRepo;
    _zainCashPaymentService = zainCashPaymentService;
  }

  // This method violates the SR (single responsibility) principle
  public async Task<bool> SetCartItemsAsync(
    Guid UserID,
    List<CartItemDto> cartItemsDto,
    CancellationToken? ct)
  {
    try
    {
      // Remove customizations: it will be removed by the db per ON DELETE CASCADE constraint
      //foreach (CartItemDto item in cartItemsDto)
      //{
      //  await _customizationRepo.DeleteByItemIDAsync(item.orderItem.Id);
      //}

      // Remove items
      await RemoveAllItemsAsync(UserID, null);

      // Set new order items
      OrderItem orderItem = new OrderItem();
      foreach (CartItemDto item in cartItemsDto)
      {
        orderItem = CartItemMapper.toEntity(item);

        // Here, using EF instea of dapper is a lot more convenient
        // Update this method so it takes the order choices with the entity that it creates
        orderItem.Id = await _orderItemRepo.CreateAsync(orderItem, ct);
      
      }
      return true;
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex.Message);
      return false;
    }
  }

  public async Task<bool> AddCartItemsAsync(Guid UserID, List<CartItemDto> cartItemsDto, CancellationToken? ct)
  {
    try
    {
      //Customization customization;

      int orderitem;
      foreach (CartItemDto item in cartItemsDto)
      {
        orderitem = await _orderItemRepo.CreateAsync(CartItemMapper.toEntity(item),ct);

      }
      return true;
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex.Message);
      return false;
    }
  }

  public async Task<string?> PlaceOrder(Guid UserID, OrderDto orderdto, 
    enPaymentMethod enPaymentMethod, CancellationToken ct)
  {
    try
    {
      // A user is a customer if he placed an order!
      int CustomerID = await _customerRepo.CreateAsync(new Customer() 
      { IsActive = true ,UserId = UserID, TurnedInAt = DateTime.Today }, ct);

      Order order = OrderMapper.toEntity(orderdto);
      order.Id = await _orderRepo.CreateAsync(order, ct);

      string paymentUrl = "";
      switch (enPaymentMethod)
      {
        case enPaymentMethod.MasterVisa:

          Address address = await _addressRepo.GetByIDAsync(order.ShippingAddressId)
            ?? throw new InvalidOperationException($"Shipping address with id: {order.ShippingAddressId} not found.");

          User? user = await _userRepo.GetByIDAsync(UserID, ct);

          paymentUrl = await _paytabPaymentService.GenereateTransactionURL(order, CustomerDetailsMapper);
          return paymentUrl;
        case enPaymentMethod.ZainCash:
          paymentUrl = await _zainCashPaymentService.GenerateZainCashPaymentURL(order);
          return paymentUrl;
        case enPaymentMethod.Other:
          throw new NotImplementedException();
        default:
          return paymentUrl;

      }
    }
    catch (Exception ex)
    {
      Log.Error(ex.Message, "Error occurred while placing order.");
      throw;
    }

  }

  public Task<List<CartItemDto>?> LoadCartItems(Guid UserID, CancellationToken ct)
  {
    //return await _cartRepo.GetCartItemsAsync(UserID);
    throw new NotImplementedException();
  }

  public async Task<bool> RemoveAllItemsAsync(Guid UserID, CancellationToken? ct)
  {
    int cartID = await _cartRepo.CreateAsync(UserID);

    return await _cartRepo.RemoveCartItemsAsync(cartID);
  }

  public async Task<bool> RemoveItemsFromCartAsync(Guid UserID, List<int> ItemsIDs, CancellationToken? ct)
  {
    int cartid = await _cartRepo.CreateAsync(UserID, ct);
    return await _cartRepo.RemoveCartItemsAsync(ItemsIDs, cartid);
  }

}
