

using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.InterfacesAndServices;
public interface ICartService
{
  /// <summary>
  /// Creates an order for this user's cart
  /// </summary>
  /// <param name="orderToPlace"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  public Task<int> PlaceOrder(Order orderToPlace, CancellationToken ct);

  /// <summary>
  /// Retruns items in the cart if any
  /// </summary>
  /// <param name="ct"></param>
  /// <returns></returns>
  public Task<List<OrderItem>?> LoadCartItems(Guid UserID, CancellationToken ct);

  /// <summary>
  /// Removes any items (if any), and set provided items instead. If orderItems is null, the cart only gets empty
  /// </summary>
  /// <param name="orderItems"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  public Task<bool> SetCartItemsAsync(Guid UserID, List<CartItem> orderItems, CancellationToken ct);

  /// <summary>
  /// Creates a new cart for the user
  /// </summary>
  /// <param name="ct"></param>
  /// <returns></returns>
  /// 
  //public Task<int?> CreateCartAsync(Guid? UserID, CancellationToken ct);


  /// <summary>
  /// Removes all items from the cart
  /// </summary>
  /// <param name="UserID"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  ///
  public Task RemoveAllItemsAsync(int shoppingCartID, CancellationToken ct);
}
