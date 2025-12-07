using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.InterfacesAndServices.Products;

namespace OnlineStore.Web.Controllers;
[Route("api/Product")]
[ApiController]
public class ProductController : ControllerBase
{
  private readonly IProductService _productService;
  public ProductController(IProductService productService)
  {
    _productService = productService;
  }

  [Authorize(Roles = "Admin")]
  [HttpGet("AllProducts", Name = "GetAllProductsAsync")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetAllProductsAsync()
  {
    var result = await _productService.GetAsync();
    return Ok(result);
  }

  [HttpPost("Create", Name = "AddProductAsync")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> AddProductAsync([FromBody] ProductDto NewProduct)
  {
    int id;

    ProductDto? product = await _productService.GetByNameAsync(NewProduct.Name);
    if (product != null)
    {
      id = await _productService.CreateNewProduct(NewProduct, null);
      return Ok("Product" + NewProduct.Name + "Is activated, Id: " + id);
    }

    if ((id = await _productService.CreateNewProduct(NewProduct, null)) != 0)
      return Ok("Product created, ID: " + id);
    else
      return StatusCode(StatusCodes.Status500InternalServerError);
  }

  [HttpDelete("{ID}", Name = "DeleteProduct")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> DeleteProductAsync(int ID)
  {
    try
    {
      await _productService.DeleteAsync(ID);

      return Ok("Product deleted");
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }

  [HttpGet("CustomProducts", Name = "GetCustomizableProducts")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> GetCustomizableProducts()
  {

    List<ProductDto>? result = await _productService.GetCustomizableProducts();

    if (result == null) return Empty;

    return Ok(result);
  }

}
