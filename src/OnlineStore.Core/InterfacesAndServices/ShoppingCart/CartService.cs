using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.Payment;
using Serilog;

namespace OnlineStore.Core.InterfacesAndServices;
public class CartService : ICartService
{
  private readonly IOrderRepo _orderRepo;
  private readonly IOrderItemRepo _orderItemRepo;
  private readonly IProductRepo _productRepo;
  private readonly IPaymentService _paymentService;
  ICartRepo _cartRepo;
  ICustomizationRepo _customizationRepo;

  public CartService(
      ICartRepo cartRepo,
      IOrderRepo orderService,
      IOrderItemRepo orderItem,
      IProductRepo productService,
      ICustomizationRepo customizationRepo,
      IPaymentService paymentService)
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
  /// <param name="CartItems"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  public async Task<bool> SetCartItemsAsync(
    Guid UserID,
    List<CartItem> CartItems,
    CancellationToken ct)
  {
    try
    {

      // Get the user's cart id 
      int? ShoppingCartID = await _cartRepo.CreateAsync(UserID, ct);

      if (ShoppingCartID == null)
        return false;

      // Remove current items from the cart and all item-related customizations
      await RemoveAllItemsAsync(ShoppingCartID.Value, ct);

      OrderItem orderItem = new OrderItem();
      Customization customization;
      foreach (CartItem item in CartItems)
      {
        orderItem = new OrderItem()
        {
          ProductID = item.ProductID,
          Quantity = item.Quantity,
          ShoppingCartID = ShoppingCartID,
          // Price is set on the DB level
        };

        orderItem.Id = await _orderItemRepo.CreateAsync(orderItem, ct);

        // Add customizations for OrderItem
        if (item.ChoicesID != null)
        {
          foreach (int choiceID in item.ChoicesID)
          {
            customization = new Customization()
            {
              CustomizationChoiceID = choiceID,
              ItemID = orderItem.Id,
            };

            customization.ID = await _customizationRepo.CreateAsync(customization);
          }
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
