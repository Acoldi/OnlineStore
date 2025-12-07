using Moq;
using OnlineStore.Core.DTOs.Items;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.PaymentServices;
using OnlineStore.Core.Mappers.ItemMappers;
using Serilog;

namespace OnlineStoreTests;

public class CartServiceTest
{
  public CartServiceTest()
  {
    Log.Logger = new LoggerConfiguration()
   .MinimumLevel.Debug()
   .WriteTo.Console()
   .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, buffered: false)
   .WriteTo.TestCorrelator()
   .CreateLogger();
  }


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

    CartRepo_Stub.Setup(c => c.RemoveCartItemsAsync(cartItems[0].ShoppingCartId))
      .ReturnsAsync(() => true);

    Mock<IOrderItemRepo> orderItemRepo_Stub = new Mock<IOrderItemRepo>();
    orderItemRepo_Stub.Setup(c => c.CreateAsync(CartItemMapper.toEntity(cartItems[0]), null))
      .ReturnsAsync(() => orderItemID);

    CartRepo_Stub.Setup(c => c.RemoveCartItemsAsync(cartItems[0].ShoppingCartId))
      .ReturnsAsync(() => { return true; });

    Mock<IProductRepo> IProductRepo_mock = new Mock<IProductRepo>();
    Mock<IOrderRepo> OrderRepo_stub = new Mock<IOrderRepo>();
    Mock<ICustomerRepo> customerRepo_stub = new Mock<ICustomerRepo>();
    Mock<IAddressRepo> addressRepo_stub = new Mock<IAddressRepo>();
    Mock<IPaymentService> paytab_stub = new Mock<IPaymentService>();
    Mock<IUserRepo> userrepo_stub = new Mock<IUserRepo>();


    CartService cartService = new CartService(CartRepo_Stub.Object, OrderRepo_stub.Object, orderItemRepo_Stub.Object
      , customerRepo_stub.Object, addressRepo_stub.Object,
        paytab_stub.Object, userrepo_stub.Object);

    // Act
    bool result = await cartService.SetCartItemsAsync(userID, cartItems, null);

    // Assert
    Assert.True(result);
    CartRepo_Stub.Verify(c => c.CreateAsync(userID, null), Times.Once);
    CartRepo_Stub.Verify(c => c.RemoveCartItemsAsync(cartItems[0].ShoppingCartId), Times.Once);
  }
}
