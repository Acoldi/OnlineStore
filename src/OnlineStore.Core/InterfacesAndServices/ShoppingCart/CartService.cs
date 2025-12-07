using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.DTOs.Items;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.PaymentServices;
using OnlineStore.Core.Mappers;
using OnlineStore.Core.Mappers.ItemMappers;
using Serilog;

namespace OnlineStore.Core.InterfacesAndServices;
public class CartService : ICartService
{
  private readonly IOrderRepo _orderRepo;
  private readonly IOrderItemRepo _orderItemRepo;
  private readonly ICartRepo _cartRepo;
  private readonly ICustomerRepo _customerRepo;
  private readonly IAddressRepo _addressRepo;
  private readonly IPaymentService _paymentService;
  private readonly IUserRepo _userRepo;

  public CartService(
      ICartRepo cartRepo,
      IOrderRepo orderRepo,
      IOrderItemRepo orderItem,
      ICustomerRepo customerRepo,
      IAddressRepo addressRepo,
      [FromKeyedServices("PayTab")] IPaymentService paytabPaymentService,
      IUserRepo userRepo)
  {
    _orderRepo = orderRepo;
    _orderItemRepo = orderItem;
    _cartRepo = cartRepo;
    _customerRepo = customerRepo;
    _addressRepo = addressRepo;
    _paymentService = paytabPaymentService;
    _userRepo = userRepo;
  }

  public async Task<bool> SetCartItemsAsync(
    Guid UserID,
    List<CartItemDto> cartItemsDto,
    CancellationToken? ct)
  {
    try
    {
      Log.Logger.Information("All Items will be removed");
      // Remove items
      if (!await RemoveAllItemsAsync(UserID, null))
      {
        Log.Logger.Information("Error removing items");
        return false;
      }
      Log.Logger.Information("All Items remvoed");


      // Set new order items
      OrderItem orderItem = new OrderItem();
      foreach (CartItemDto OrderItem in cartItemsDto)
      {
        orderItem = CartItemMapper.toEntity(OrderItem);

        // No need to add it, since I'm using EF in _orderItemRepo.CreateAsync()
        //orderItem.CustomizationChoices.Add(_customizationChoiceRepo.GetByIDAsync(item.ChoicesID));

        orderItem.Id = await _orderItemRepo.CreateAsync(orderItem, ct);

      }
      return true;
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex, " Why not publishing??? " + ex.Message);
      return false;
    }
  }

  public async Task<bool> AddCartItemsAsync(Guid UserID, List<CartItemDto> cartItemsDto, CancellationToken? ct)
  {
    try
    {
      int AddedOrders = 0;

      List<OrderItem> orderItems = new List<OrderItem>();
      foreach (CartItemDto item in cartItemsDto)
      {
        orderItems.Add(CartItemMapper.toEntity(item));
      }

      AddedOrders = await _orderItemRepo.CreateAsync(orderItems, ct);

      return AddedOrders > 0;
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
      { IsActive = true, UserId = UserID, TurnedInAt = DateTime.Today }, ct);

      Order order = OrderMapper.toEntity(orderdto);
      order.Id = await _orderRepo.CreateAsync(order, ct);

      string paymentUrl = "";
      switch (enPaymentMethod)
      {
        case enPaymentMethod.MasterVisa:

          Address address = await _addressRepo.GetByIDAsync(order.ShippingAddressId)
            ?? throw new InvalidOperationException($"Shipping address with id: {order.ShippingAddressId} not found.");

          User? user = await _userRepo.GetByIDAsync(UserID, ct);

          paymentUrl = await _paymentService.GenereateTransactionURL(order);
          return paymentUrl;
        case enPaymentMethod.ZainCash:
          paymentUrl = await _paymentService.GenereateTransactionURL(order);
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
    try
    {
      Log.Logger.Error("UserID: " + UserID.ToString());

      int cartID = await _cartRepo.CreateAsync(UserID);

      return await _cartRepo.RemoveCartItemsAsync(cartID);
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex, ex.Message);
      Environment.Exit(1);
      throw;
    }
  }

  public async Task<bool> RemoveItemsFromCartAsync(Guid UserID, List<int> ItemsIDs, CancellationToken? ct)
  {
    int cartid = await _cartRepo.CreateAsync(UserID, ct);
    return await _cartRepo.RemoveCartItemsAsync(ItemsIDs, cartid);
  }

}
