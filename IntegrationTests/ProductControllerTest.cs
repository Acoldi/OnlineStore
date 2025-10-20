using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OnlineStore.Core.Entities;
using Serilog;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace IntegrationTests;

public class ProductControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _applicationFactory;
  private readonly ITestOutputHelper _TestOutputHelper;

  public ProductControllerTest(WebApplicationFactory<Program> applicationFactory, ITestOutputHelper testOutputHelper)
  {
    _applicationFactory = applicationFactory.WithWebHostBuilder(
      (configuration) => configuration.UseEnvironment(Environments.Development));
    _TestOutputHelper = testOutputHelper;

    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
  }

  [Fact]
  public async Task Get_AllProducts()
  {
    // Arrange
    HttpClient httpClient = _applicationFactory.CreateClient();

    // Act
    HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/Product/AllProducts");

    // Assert
    if (!httpResponseMessage.IsSuccessStatusCode)
    {
        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
        _TestOutputHelper.WriteLine($"Request did faile with status code {httpResponseMessage
          .StatusCode} and response: {responseContent}");

      
    }
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
      CategoryId = 1,
      
    };

    string content = JsonConvert.SerializeObject(product);

    StringContent stringContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");

    HttpResponseMessage responseMessage = await httpClient.PostAsync("api/Product/Create", stringContent);

    // Assert
    _TestOutputHelper.WriteLine("Request status: " + responseMessage.StatusCode + " With content: " 
      + await responseMessage.Content.ReadAsStringAsync());
    Assert.NotNull(responseMessage.Content);
    Assert.True(responseMessage.IsSuccessStatusCode);
  }
}
