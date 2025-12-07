using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text.Json;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.DTOs.Items;
using OnlineStore.Core.Entities;
using OnlineStore.Infrastructure.Data.Models;

namespace IntegrationTests;

[Collection("AllTests")]
public class CartControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>,
  IClassFixture<SharedDataFixture>
{
  private readonly SharedDataFixture _fixture;
  private readonly HttpClient _httpClient;
  private readonly DataBaseFixture _dataBaseFixture;

  public CartControllerTest(CustomWebApplicationFactory<Program> customWebApplicationFactory
    , SharedDataFixture fixture
    , DataBaseFixture dataBaseFixture)
  {
    _fixture = fixture;
    _httpClient = customWebApplicationFactory.CreateClient();
    _dataBaseFixture = dataBaseFixture;

    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
  }

  [Fact]
  public async Task SetCartItemsAsync_returnsOk()
  {
    //Arrange

    Console.WriteLine("USER ID: " + _dataBaseFixture.TestUser.Id + '\n' + " User Email: " + _dataBaseFixture.TestUser.EmailAddress);
    List<CartItemDto> cartItemDtos = new List<CartItemDto>()
    {
      new CartItemDto()
      {
        ProductId = 2,
        ShoppingCartId = _dataBaseFixture.TestUser.ShoppingCart?.Id ?? throw new InvalidOperationException("Test user doesn't exist"),
        Quantity = 3,
        ChoicesID = new List<int> { 2 },
      },
    };

    Console.WriteLine("Shopping cart ID from cartItemDto: " + cartItemDtos[0].ShoppingCartId ?? "SHopping cart is not set");

    StringContent cartItems = new StringContent(JsonSerializer.Serialize(cartItemDtos, _fixture.jsonSerializerOptions));
    cartItems.Headers.ContentType = new MediaTypeHeaderValue(ContentType.ApplicationJson.ToString());


    //Act
    HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("api/Cart/SetItems", cartItems);

    string content = await httpResponseMessage.Content.ReadAsStringAsync();

    //Assert
    Console.WriteLine("Status Code: " + httpResponseMessage.StatusCode + '\n');
    Console.WriteLine("Response Phrase: " + httpResponseMessage.ReasonPhrase + '\n');
    Console.WriteLine("Content: " + content);
    Assert.True(httpResponseMessage.IsSuccessStatusCode);

  }

  [Fact]
  public async Task cartController_PlaceOrder_ReturnsARedirectResponse()
  {

  }
}
