using Moq;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.Payment;
using OnlineStore.Web.DTOs;

namespace OnlineStoreTests;

public class CartTesting
{
  [Fact]
  public async Task SetCartItemsAsync_WhenCalled_SetsNewItemsCorrectly()
  {
    // Arrange
    Guid userID = Guid.NewGuid();
    List<CartItemDto> cartItems = new List<CartItemDto>()
    {
      new CartItemDto()
      {
        ChoicesID = [1],
        orderItem = new OrderItem()
        {
          Id = 9,
          ShoppingCartID = 10,
          Quantity = 1,
          Price = 100,
        }
      }
    };

    User user = new User(false, "Ali", "Hazem", "an36", null, null, "Password")
    {
      ID = userID,
      Password = "password",
      EmailAddress = "an"
    };

    Mock<ICartRepo> CartRepo_mock = new Mock<ICartRepo>();
    CartRepo_mock.Setup(c => c.CreateAsync(userID, null)).ReturnsAsync(() => cartItems[0].orderItem.ShoppingCartID);


    Mock<ICustomizationRepo> customizationRepo_mock = new Mock<ICustomizationRepo>();
    customizationRepo_mock.Setup(c => c.DeleteByItemIDAsync(cartItems[0].orderItem.Id, null)).ReturnsAsync(() => true);
      customizationRepo_mock.Setup(c => c.CreateAsync(new Customization()
      {
        CustomizationChoiceID = cartItems[0].ChoicesID[0],
        OrderItemID = cartItems[0].orderItem.Id,
      }, 
      null)).ReturnsAsync(() => 1);

    CartRepo_mock.Setup(c => c.RemoveCartItemsAsync(cartItems[0].orderItem.ShoppingCartID ?? 10)).ReturnsAsync(() => true);

    Mock<IOrderItemRepo> IorderItemRepo_mock = new Mock<IOrderItemRepo>();
    IorderItemRepo_mock.Setup(c => c.CreateAsync(cartItems[0].orderItem, null)).ReturnsAsync(() => cartItems[0].orderItem.Id);

    Mock<IProductRepo> IProductRepo_mock = new Mock<IProductRepo>();
    Mock<IZainCashPaymentService> zaincashpaymentservice_mock = new Mock<IZainCashPaymentService>();
    Mock<IOrderRepo> IOrderRepo_mock = new Mock<IOrderRepo>();

    CartService cartService = new CartService(CartRepo_mock.Object, IOrderRepo_mock.Object, IorderItemRepo_mock.Object, IProductRepo_mock.Object, customizationRepo_mock.Object,
      zaincashpaymentservice_mock.Object);

    // Act
    bool results = await cartService.SetCartItemsAsync(userID, cartItems, null);

    // Assert
    Assert.True(results);
  }
}
