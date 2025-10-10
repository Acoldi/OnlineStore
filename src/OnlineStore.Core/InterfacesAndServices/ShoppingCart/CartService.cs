using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.Payment;
using OnlineStore.Web.DTOs;
using Serilog;

namespace OnlineStore.Core.InterfacesAndServices;
public class CartService : ICartService
{
  private readonly IOrderRepo _orderRepo;
  private readonly IOrderItemRepo _orderItemRepo;
  private readonly IProductRepo _productRepo;
  private readonly IZainCashPaymentService _paymentService;
  ICartRepo _cartRepo;
  ICustomizationRepo _customizationRepo;

  public CartService(
      ICartRepo cartRepo,
      IOrderRepo orderService,
      IOrderItemRepo orderItem,
      IProductRepo productService,
      ICustomizationRepo customizationRepo,
      IZainCashPaymentService paymentService)
  {
    _orderRepo = orderService;
    _orderItemRepo = orderItem;
    _productRepo = productService;
    _cartRepo = cartRepo;
    _customizationRepo = customizationRepo;
    _paymentService = paymentService;
  }
  /// <summary>
  /// Removes all items in the user cart and add new orderItems
  /// </summary>
  /// <param name="UserID"></param>
  /// <param name="cartItemsDto"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  public async Task<bool> SetCartItemsAsync(
    Guid UserID,
    List<CartItemDto> cartItemsDto,
    CancellationToken ct)
  {
    try
    {
      // Getting the user's cart ID.
      int? ShoppingCartID = await _cartRepo.CreateAsync(UserID, ct);

      if (ShoppingCartID == null)
        return false;

      // Remove customizations
      foreach (CartItemDto item in cartItemsDto)
      {
        await _customizationRepo.DeleteByItemIDAsync(item.orderItem.Id);
      }

      // Remove items
      await RemoveAllItemsAsync(ShoppingCartID.Value, ct);

      // Set new order items
      OrderItem orderItem;
      Customization customization;
      foreach (CartItemDto item in cartItemsDto)
      {
        orderItem = new OrderItem()
        {
          OrderID = item.orderItem.Id,
          ProductID = item.orderItem.ProductID,
          Quantity = item.orderItem.Quantity,
          ShoppingCartID = ShoppingCartID,
        };
        orderItem.Id = await _orderItemRepo.CreateAsync(orderItem, ct);

        // Add customizations for OrderItem
        foreach (int choiceid in item.ChoicesID)
        {
          customization = new Customization(choiceid, orderItem.Id, orderItem.);

          await _customizationRepo.CreateAsync(customization, ct);
        }
      }
      return true;
    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex.Message);
      return false;
    }
  }


  public async Task<int> PlaceOrder(Order order, CancellationToken ct)
  {
    try
    {
      order.ID = await _orderRepo.CreateAsync(order, ct);
    }
    catch (Exception ex)
    {
      Log.Error(ex, "An error occurred while placing an order.");
      throw;
    }

    return order.ID;
  }

  public async Task<List<OrderItem>?> LoadCartItems(Guid UserID, CancellationToken ct)
  {
    return await _cartRepo.GetCartItemsAsync(UserID);
  }

  public async Task RemoveAllItemsAsync(int ShoppingCartID, CancellationToken ct)
  {
    await _cartRepo.RemoveCartItemsAsync(ShoppingCartID);
  }

}
