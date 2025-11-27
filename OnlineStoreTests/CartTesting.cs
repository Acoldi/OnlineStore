using Moq;
using OnlineStore.Core.DTOs.Items;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.PaymentServices;

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
        ChoicesID = new List<int> { 1 },
        ProductId = 9,
        ShoppingCartId = 10,
        Quantity = 1,
      }
    };
    int orderItemID = 1;
    User user = new User()
    {
      Id = userID,
      Password = "password",
      EmailAddress = "an",
      FirstName = "Alllli",
    };

    Mock<ICartRepo> CartRepo_Stub = new Mock<ICartRepo>();
    CartRepo_Stub.Setup(c => c.CreateAsync(userID, null)).ReturnsAsync(cartItems[0].ShoppingCartId);

    CartRepo_Stub.Setup(c => c.RemoveCartItemsAsync(new List<int> { 9 }, 10)).ReturnsAsync(() => { return true; });

    Mock<IOrderItemRepo> orderItemRepo_Stub = new Mock<IOrderItemRepo>();
    orderItemRepo_Stub.Setup(c => c.CreateAsync(new OrderItem() 
    { 
      ShoppingCartId = cartItems[0].ShoppingCartId,
      ProductId = cartItems[0].ProductId,
      Quantity = cartItems[0].Quantity,
    }
    , null)).ReturnsAsync(() => orderItemID);

    Mock<ICustomizationRepo> customizationRepo_stub = new Mock<ICustomizationRepo>();
    customizationRepo_stub.Setup(c => c.CreateAsync(new Customization()
    {
      CustomizationChoiceId = cartItems[0].ChoicesID[0],
      ItemId = cartItems[0].orderItem.Id,
    },
    null)).ReturnsAsync(() => 1);

    CartRepo_Stub.Setup(c => c.RemoveCartItemsAsync(cartItems[0].orderItem.ShoppingCartId ?? 10)).ReturnsAsync(() => true);

    Mock<IProductRepo> IProductRepo_mock = new Mock<IProductRepo>();
    Mock<IOrderRepo> OrderRepo_stub = new Mock<IOrderRepo>();
    Mock<ICustomerRepo> customerRepo_stub = new Mock<ICustomerRepo>();
    Mock<IAddressRepo> addressRepo_stub = new Mock<IAddressRepo>();
    Mock<IPaymentService> paytab_stub = new Mock<IPaymentService>();
    Mock<IUserRepo> userrepo_stub = new Mock<IUserRepo>();
    Mock<IZainCashPaymentService> zaincashservice_stub = new Mock<IZainCashPaymentService>();


    CartService cartService = new CartService(CartRepo_Stub.Object, OrderRepo_stub.Object, orderItemRepo_Stub.Object,
        customizationRepo_stub.Object, customerRepo_stub.Object, addressRepo_stub.Object, 
        paytab_stub.Object, userrepo_stub.Object, zaincashservice_stub.Object);

    // Acte
    bool results = await cartService.SetCartItemsAsync(userID, cartItems, null);

    // Assert
    Assert.True(results);
    CartRepo_Stub.Verify(c => c.CreateAsync(userID, null), Times.Once);
    CartRepo_Stub.Verify(c => c.RemoveCartItemsAsync(cartItems[0].orderItem.ShoppingCartId!.Value), Times.Once);
    orderItemRepo_Stub.Verify(c => c.CreateAsync(cartItems[0].orderItem, null), Times.Once);
    
    customizationRepo_stub.Verify(c => c.CreateAsync(It.Is<Customization>(c =>
      c.CustomizationChoiceId == cartItems[0].ChoicesID[0] &&
      c.ItemId == cartItems[0].orderItem.Id),
    null), Times.Once);
  }
}
