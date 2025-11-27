using System.Net.Http.Headers;
using System.Text;
using AngleSharp.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using OnlineStore.Core.DTOs;
using OnlineStore.Web.DTOs;
using Serilog;
using Xunit.Abstractions;

namespace IntegrationTests;

public class ProductControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly ITestOutputHelper _TestOutputHelper;
  private readonly WebApplicationFactory<Program> _factory;

  public ProductControllerTest(CustomWebApplicationFactory<Program> applicationFactory, ITestOutputHelper testOutputHelper)
  {
    _factory = applicationFactory;

    _TestOutputHelper = testOutputHelper;

    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.TestOutput(testOutputHelper)
        .CreateLogger();
  }

  [Fact]
  public async Task GetAsync_ListAllProducts()
  {
    // Arrange
    HttpClient client = _factory.CreateClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

    // Act
    HttpResponseMessage httpResponseMessage = await client.GetAsync("api/Product/AllProducts");

    // Assert
    if (!httpResponseMessage.IsSuccessStatusCode)
    {
      var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
      _TestOutputHelper.WriteLine($"Request failed with status code {httpResponseMessage
        .StatusCode} and response: {responseContent}");
    }

    _TestOutputHelper.WriteLine("Suucceeeessss");
    _TestOutputHelper.WriteLine(await httpResponseMessage.Content.ReadAsStringAsync());
    Console.Write(" SEPORATION \n", await httpResponseMessage.Content.ReadAsStringAsync());
    Assert.True(httpResponseMessage.IsSuccessStatusCode);
  }

  [Fact]
  public async Task PostAsync_Product_CreatesProductAndReturnsItsID()
  {
    // Arrange
    HttpClient httpClient = _factory.CreateClient();
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");


    // Act
    AddProductParameters product = new AddProductParameters()
    {
      ProductName = "NewlyAddedYYYY",
      CategoryID = 1,
      Price = 100,
      Description = "Description",
      CustomizationOptionID = 1,
      
    };
    HttpContent content = new StringContent
      (System.Text.Json.JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

    HttpResponseMessage responseMessage = await httpClient.PostAsync("api/Product/Create",
          content);

    // Assert
    _TestOutputHelper.WriteLine("Request status: " + responseMessage.StatusCode + " With content: "
      + await responseMessage.Content.ReadAsStringAsync());
    Console.WriteLine("New Product ID: " + responseMessage.Content.ReadAsStringAsync());
    Assert.NotNull(responseMessage.Content);
    Assert.True(responseMessage.IsSuccessStatusCode);
  }
}
