using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OnlineStore.Core.Entities;

namespace IntegrationTests;

public class ProductControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _applicationFactory;

  public ProductControllerTest(WebApplicationFactory<Program> applicationFactory)
  {
    _applicationFactory = applicationFactory.WithWebHostBuilder(
      (configuration) => configuration.UseEnvironment(Environments.Development));
  }

  [Fact]
  public async Task Get_CreatesNewProductReturnsItsID()
  {
    // Arrange
    HttpClient httpClient = _applicationFactory.CreateClient();

    // Act
    HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("/api/product/AllProducts");

    // Assert
    Assert.NotNull(httpResponseMessage.Content);
    Assert.True(httpResponseMessage.IsSuccessStatusCode);
  }

  [Fact]
  public async Task Put_Creating_Aproduct()
  {
    // Arrange
    HttpClient httpClient = _applicationFactory.CreateClient();

    // Act
    Product product = new Product()
    {
      Name = "NewlyAdded",
      Sku = "NewlyAdded",
    };

    string content = JsonConvert.SerializeObject(product);

    StringContent stringContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");

    HttpResponseMessage responseMessage = await httpClient.PostAsync("/api/Product/Create", stringContent);

    // Assert
    Assert.NotNull(responseMessage.Content);
    Assert.True(responseMessage.IsSuccessStatusCode);
  }
}
