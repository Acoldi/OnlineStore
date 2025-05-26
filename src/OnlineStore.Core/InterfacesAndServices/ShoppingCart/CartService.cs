using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices.DTOs;
using OnlineStore.Core.InterfacesAndServices.DTOs.Parameters;
using OnlineStore.Core.InterfacesAndServices.Order;
using OnlineStore.Core.InterfacesAndServices.OrderItem;
using Serilog;

namespace OnlineStore.Core.InterfacesAndServices.ShoppingCart;
public class CartService : ICartService
{
  private readonly List<CartItem> _cartItems;
  private readonly IOrderService _orderService;
  private readonly IOrderItemService _orderItem;
  private readonly IDataAccess _dataAccess;
  public CartService(IOrderService orderService, IOrderItemService orderItem, IDataAccess dataAccess)
  {
    _orderService = orderService;
    _orderItem = orderItem;
    _dataAccess = dataAccess;

    _cartItems = new List<CartItem>();
  }

  public Task AddItem(List<CartItem> orderItem)
  {
    _cartItems.AddRange(orderItem);
    return Task.CompletedTask;
  }

  public Task Remove(int productID)
  {
    _cartItems.RemoveAll(c => c.ProductID == productID);

    return Task.CompletedTask;
  }

  /// <summary>
  /// Save User's items in cart
  /// </summary>
  /// <param name="UserID"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  public async Task AddItemsToCart(int UserID, CancellationToken ct)
  {
    int UserCartID = await _dataAccess.CreateAsync<Guid>("SP_CreateShoppingCart", System.Data.CommandType.StoredProcedure, ct, UserID);

    foreach (CartItem item in _cartItems)
    {
      await _orderItem.create(new CreateOrderItemParam() {
        ShoppingCartID = UserCartID,
        Price = item.PriceSnapShot,
        ProductID = item.ProductID,
        Quantity = item.Quantity
      },
      ct);
    }
  }

  public async Task PlaceOrder(CreateOrderParam createOrderParam, CancellationToken ct)
  {
    int OrderId = 0;
    try
    {
      OrderId = await _orderService.Creaate(createOrderParam, ct);
    }
    catch (Exception ex)
    {
      Log.Error(ex, "An error occurred while placing an order.");
      throw;
    }

    foreach (CartItem item in _cartItems)
    {
      await _orderItem.create(new CreateOrderItemParam() {
        OrderID = OrderId,
        Price = item.PriceSnapShot,
        ProductID = item.ProductID,
        Quantity = item.Quantity
      },
      ct);

    }
  }

  public async Task<List<CartItem>> LoadCartItems(int customerID, CancellationToken ct)
  {
    return await _dataAccess.LoadDataAsync<CartItem>()
  }

}
