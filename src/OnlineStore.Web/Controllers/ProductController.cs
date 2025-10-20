using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.Products;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Web.Controllers;
[Route("api/Product")]
[ApiController]
public class ProductController : ControllerBase
{
  IProductService _productService;
  IProductRepo _productRepo;
  public ProductController(IProductRepo productRepo ,IProductService productService)
  {
    _productService = productService;
    _productRepo = productRepo;
  }

  //[Authorize(Roles = "Admin")]
  [HttpGet("AllProducts", Name = "GetAllProducts")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetAllProductsAsync()
  {
    var result = await _productRepo.GetAsync();
    return Ok(result);
  }

  [HttpPost("Create", Name = "AddProduct")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  //[Authorize(Roles = "Admin")]
  public async Task<IActionResult> AddProductAsync(AddProductParameters NewProduct, CancellationToken? ct)
  {
    Product? product = await _productRepo.GetByNameAsync(NewProduct.ProductName);
    if (product != null)
      return BadRequest("there is already a product named: " + NewProduct.ProductName);

    
    if (await _productService.CreateNewProduct(NewProduct, ct) != 0)
      return Ok("Product created");
    else
      return StatusCode(StatusCodes.Status500InternalServerError);
  }

  [HttpDelete("{ID}", Name = "DeleteProduct")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> DeleteProductAsync(int ID)
  {
    await _productRepo.DeleteAsync(ID);
    
    return Ok("Product deleted");
  }

  [HttpGet("Custom products", Name = "GetCustomizableProducts")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> GetCustomizableProducts(int ID)
  {

    List<Product>? result = await _productRepo.GetCustomizableProducts(null);
    
    return Ok(result);
  }


}
