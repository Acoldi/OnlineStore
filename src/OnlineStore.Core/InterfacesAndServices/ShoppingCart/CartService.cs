using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using Serilog;

namespace OnlineStore.Core.InterfacesAndServices;
public class CartService : ICartService
{
  private readonly IOrderRepo _orderRepo;
  private readonly IOrderItemRepo _orderItemRepo;
  private readonly IProductRepo _productRepo;
  ICartRepo _cartRepo;
  ICustomizationRepo _customizationRepo;

  public CartService(
      ICartRepo cartRepo,
      IOrderRepo orderService,
      IOrderItemRepo orderItem,
      IProductRepo productService,
      ICustomizationRepo customizationRepo)
  {
    _orderRepo = orderService;
    _orderItemRepo = orderItem;
    _productRepo = productService;
    _cartRepo = cartRepo;
    _customizationRepo = customizationRepo;
  }
  /// <summary>
  /// Removes all items in the user cart and add new orderItems
  /// </summary>
  /// <param name="UserID"></param>
  /// <param name="CartItems"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  public async Task<bool> SetItemsAsync(
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

      // ToDo: Remove customizations if any!
      // Remove current items from the cart
      await RemoveAllItemsAsync(ShoppingCartID.Value, ct);

      int NewOrderItemID = 0;
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

        NewOrderItemID = await _orderItemRepo.CreateAsync(orderItem, ct);

        // Add customizations for OrderItem
        if (item.ChoicesID != null)
        {
          foreach (int choiceID in item.ChoicesID)
          {
            customization = new Customization()
            {
              CustomizationChoiceID = choiceID,
              OrderItemID = NewOrderItemID,
              ExtraCost = await _customizationRepo.GetCustomizationExtraCost(choiceID, item.Quantity)
            };

            await _customizationRepo.CreateAsync(customization);
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


  public async Task PlaceOrder(Order order, CancellationToken ct)
  {
    try
    {
      await _orderRepo.CreateAsync(order, ct);
    }
    catch (Exception ex)
    {
      Log.Error(ex, "An error occurred while placing an order.");
      throw;
    }
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
