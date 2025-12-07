using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices;
using OnlineStore.Web.DTOs;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Network;
using Serilog;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;
using OnlineStore.Infrastructure.Data.Models;
using System.Text.Json;

namespace IntegrationTests;

public class ProductControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>, IClassFixture<SharedDataFixture>
{
  private readonly HttpClient _httpClient;
  private readonly CustomWebApplicationFactory<Program> _factory;
  private readonly SharedDataFixture _fixture;

  public ProductControllerTest(CustomWebApplicationFactory<Program> applicationFactory, SharedDataFixture fixture) 
  {
    _factory = applicationFactory;

    _httpClient = _factory.CreateClient();

    _fixture = fixture;
  }

  [Fact]
  public async Task GetAsync_ListAllProducts()
  {
    // Arrange
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

    // Act
    HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/Product/AllProducts");

    // Assert
    string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

    Console.WriteLine(responseContent);

    Assert.True(httpResponseMessage.IsSuccessStatusCode);
  }

  [Fact]
  public async Task PostAsync_Product_CreatesProductAndReturnsItsID()
  {
    // Arrange
    HttpClient httpClient = _factory.CreateClient();
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");


    // Act
    ProductDto product = new ProductDto()
    {
      Name = "NewlyAddedYYYY13 The very new thing",
      CategoryId = 1,
      Price = 100,
      Description = "Description",
      Sku = "NewlyAdded",
      IsActive = true,
      Slug = UtilityService.GenerateSlug("newly addded  de"),
      CreatedAt = DateTime.UtcNow,
    };

    HttpContent content = new StringContent
      (JsonSerializer.Serialize(product, options: _fixture.jsonSerializerOptions), Encoding.UTF8, "application/json");

    HttpResponseMessage responseMessage = await httpClient.PostAsync("api/Product/Create",
          content);

    // Assert
    Console.WriteLine("Request status: " + responseMessage.StatusCode + " With content: "
      + await responseMessage.Content.ReadAsStringAsync());

    Assert.NotNull(responseMessage.Content);
    Assert.True(responseMessage.IsSuccessStatusCode);
  }
}
