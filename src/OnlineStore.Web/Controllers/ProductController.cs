using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DTOs.Parameters;
using OnlineStore.Core.Interfaces.Products;

namespace OnlineStore.Web.Controllers;
[Route("api/Product")]
[ApiController]
public class ProductController : ControllerBase
{
  IProductService _productService;
  public ProductController(IProductService productService)
  {
    _productService = productService;
  }

  [Authorize(Roles = "Admin")]
  [HttpGet("All products", Name = "GetAllProducts")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetAllProductsAsync()
  {
    var result = await _productService.GetAll();
    return Ok(result);
  }

  [HttpPost("Add product", Name = "AddProduct")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> AddProductAsync(AddProductParameters NewProduct)
  {
    if (await _productService.Create(NewProduct) != 0)
      return Ok("Product created");
    else
      return StatusCode(StatusCodes.Status500InternalServerError);
  }

  [HttpDelete("{ID}", Name = "DeleteProduct")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> DeleteProductAsync(int ID)
  {

    await _productService.Delete(ID);
    
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
