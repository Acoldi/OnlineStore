using OnlineStore.Core.DTOs;
using OnlineStore.Core.InterfacesAndServices;

namespace OnlineStoreTests;

public class CartTesting
{
  [Fact]
  public void SetCartItemsAsync_WhenCalled_SetsNewItemsCorrectly()
  {
    // Arrange
    Guid userID = Guid.NewGuid();
    List<CartItem> cartItems = new List<CartItem>()
    {
      new CartItem()
      {
        ProductID = 9,
        Quantity = 1,
      }
    };

    // Act
    CartService.
  }
}
