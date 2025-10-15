using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Web.Controllers;
[Route("api/Product")]
[ApiController]
public class ProductController : ControllerBase
{
  IProductRepo _productService;
  public ProductController(IProductRepo productService)
  {
    _productService = productService;
  }

  //[Authorize(Roles = "Admin")]
  [HttpGet("AllProducts", Name = "GetAllProducts")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetAllProductsAsync()
  {
    var result = await _productService.GetAsync();
    return Ok(result);
  }

  [HttpPost("Create", Name = "AddProduct")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  //[Authorize(Roles = "Admin")]
  public async Task<IActionResult> AddProductAsync(AddProductParameters NewProduct)
  {
    Product? product = await _productService.GetByNameAsync(NewProduct.ProductName);
    if (product != null)
      return BadRequest("there is already a product named: " + NewProduct.ProductName);

    if (NewProduct.CustomizationOptionID == 0)
      NewProduct.CustomizationOptionID = null;

    product = new Product()
    {
      Name = NewProduct.ProductName,
      Sku = "NotImplementedYet",
      Slug = "NotImplementedYet",
      CreatedAt = DateTime.Now,
      CustomizationOptionId = NewProduct.CustomizationOptionID,
      CategoryId = NewProduct.CategoryID,
      Description = NewProduct.Description,
      IsActive = true,
      Price = NewProduct.Price,
      Category = NewProduct.category
    };
    
    if (await _productService.CreateAsync(product) != 0)
      return Ok("Product created");
    else
      return StatusCode(StatusCodes.Status500InternalServerError);
  }

  [HttpDelete("{ID}", Name = "DeleteProduct")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> DeleteProductAsync(int ID)
  {
    await _productService.DeleteAsync(ID);
    
    return Ok("Product deleted");
  }

  [HttpGet("Custom products", Name = "GetCustomizableProducts")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> GetCustomizableProducts(int ID)
  {

    List<Product>? result = await _productService.GetCustomizableProducts(null);
    
    return Ok(result);
  }


}
