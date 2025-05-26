using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.Category;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices;
using Serilog.Core;

namespace OnlineStore.Web.Controllers;
[Route("api/Category")]
[ApiController]
public class CategoryController : ControllerBase
{
  private readonly ICategoryService _categoryService;

  public CategoryController(ICategoryService categoryService)
  {
    _categoryService = categoryService;
  }


  [HttpPost("Create")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> CreateCategory(string name, int? parentCategory)
  {
    int result = 0;
    if (parentCategory < 1)
      parentCategory = null;
    try
    {
      result = await _categoryService.CreatCategoryAsync(new Category(name, parentCategory, UtilityService.GenerateSlug(name)));
      return Ok(new { succes = result });
    }
    catch (Exception e)
    {
      return BadRequest(new { succes = $"There is already a {name} category; {e.Message}" });
    }
  }

  [HttpDelete("ID: {ID}", Name = "Delete Category")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> DeleteCategory(int ID, CancellationToken ct)
  {
    bool result = false;

    try
    {
      result = await _categoryService.DeleteCategoryAsync(ID, ct);
      
      if (result)
        return Ok("Category with the ID: " + ID + " Deleted");
      else
        return BadRequest("There is no category with ID: " + ID);
    }
    catch (Exception e)
    {
      Log.Error(e.Message);
      return BadRequest(e.Message);
    }

  }

  [HttpDelete("Name: {Name}", Name = "DeleteCategoryByName")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> DeleteCategory(string Name, CancellationToken ct)
  {
    bool result = false;

    try
    {
      result = await _categoryService.DeleteCategoryByNameAsync(Name, ct);
      
      if (result)
        return Ok("Category with name: " + Name + " Deleted");
      else
        return BadRequest("There is no category with name: " + Name);
    }
    catch (Exception e)
    {
      Log.Error(e.Message);
      return BadRequest(e.Message);
    }

  }

  [HttpPut("Update {ID}", Name = "UpdateCategory")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> UpdateCategory(int ID, Category category, CancellationToken ct)
  {
    bool result = false;

    try
    {
      result = await _categoryService.UpdateCategoryAsync(ID, category, ct);
      
      if (result)
        return Ok("Category with name: " + category.Name + " updated");
      else
        return NotFound("There is no category with name: " + category.Name);
    }
    catch (Exception e)
    {
      Log.Error(e.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }

  }
  
  [HttpGet("All", Name = "GetCategories")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> GetCategories(CancellationToken ct)
  {
    List<Category>? categories = new List<Category>();

    try
    {
      categories = await _categoryService.GetCategoriesAsync(ct);
      
      if (categories != null)
        return Ok(categories);
      else
        return NoContent();
    }
    catch (Exception e)
    {
      Log.Error(e.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });

    }

  }

  [HttpGet("{Name}", Name = "GetCategoryByName")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> GetCategoryByName(string Name, CancellationToken ct)
  {
    List<Category>? categories = new List<Category>();

    try
    {
      categories = await _categoryService.GetCategoryByName(Name, ct);
      
      if (categories != null)
        return Ok(categories);
      else
        return NoContent();
    }
    catch (Exception e)
    {
      Log.Error(e.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });

    }

  }

  [HttpGet("Under {categoryID}", Name = "GetCategoriesUnder")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> GetCategoriesUnder(int categoryID, CancellationToken ct)
  {
    List<Category>? categories = new List<Category>();

    try
    {
      categories = await _categoryService.GetCategoriesUnderAsync(categoryID, ct);
      
      if (categories != null)
        return Ok(categories);
      else
        return NoContent();
    }
    catch (Exception e)
    {
      Log.Error(e.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });

    }

  }


  
}
