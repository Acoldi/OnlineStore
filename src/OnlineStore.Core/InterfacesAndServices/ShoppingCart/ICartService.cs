

using OnlineStore.Core.DTOs;
using OnlineStore.Core.DTOs.Items;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.InterfacesAndServices;
public interface ICartService
{
  /// <summary>
  /// Creates an order
  /// </summary>
  /// <param name="orderToPlace"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  public Task<string?> PlaceOrder(Guid UserID, OrderDto orderToPlace, enPaymentMethod enPaymentMethod,CancellationToken ct);

  /// <summary>
  /// Retruns items in the cart
  /// </summary>
  /// <param name="ct"></param>
  /// <returns></returns>
  public Task<List<CartItemDto>?> LoadCartItems(Guid UserID, CancellationToken ct);

  /// <summary>
  /// Removes all items (if any), and set provided items instead.
  /// If orderItems is null, the cart only gets empty
  /// </summary>
  /// <param name="orderItems"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  public Task<bool> SetCartItemsAsync(Guid UserID, List<CartItemDto> CartItemsDto, CancellationToken? ct);

  /// <summary>
  /// Add items to user cart
  /// </summary>
  /// <param name="orderItems"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  public Task<bool> AddCartItemsAsync(Guid UserID, List<CartItemDto> CartItemsDto, CancellationToken? ct);



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
  public Task<bool> RemoveAllItemsAsync(Guid UserID, CancellationToken? ct);
  
  /// <summary>
  /// Removes items with ItemsIDs from the cart
  /// </summary>
  /// <param name="UserID"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  ///
  public Task<bool> RemoveItemsFromCartAsync(Guid UserID, List<int> ItemsIDs, CancellationToken? ct);

}
