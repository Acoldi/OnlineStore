using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices;
using OnlineStore.Core.InterfacesAndServices.CategoryService;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

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
      result = await _categoryService.CreateAsync(new CategoryDto()
      {
        Name = name,
        ParentCategoryId = parentCategory,
        Slug = name
      });

      return Ok(new { NewID = result });
    }
    catch (Exception e)
    {
      return BadRequest(new { e.Message });
    }
  }

  [HttpDelete("ID/{ID}", Name = "Delete Category")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> DeleteCategory([FromRoute]int ID, CancellationToken ct)
  {
    try
    {
      await _categoryService.DeleteAsync(ID);
      return NoContent();
    }
    catch (SqlException e)
    {
      Log.Error(e.Message);
      return BadRequest("There are either products linked to this category or it's a parent category, " +
        "resolve any related data before deleting " + e.Message);
    }

  }

  [HttpDelete("Name/{Name}", Name = "DeleteCategoryByName")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> DeleteCategory([FromRoute]string Name, CancellationToken ct)
  {
    try
    {
      await _categoryService.DeleteAsync(Name, ct);
      return NoContent();
    }
    catch (Exception e)
    {
      Log.Error(e.Message);
      return BadRequest(e.Message);
    }

  }

  [HttpPut("Update", Name = "UpdateCategory")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> UpdateCategory(CategoryDto category, CancellationToken ct)
  {
    try
    {
      await _categoryService.UpdateAsync(category);
      return NoContent();
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
    List<CategoryDto>? categories = new List<CategoryDto>();

    try
    {
      categories = await _categoryService.ListAllAsync();

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

  [HttpGet("UnderParentName/{Name}", Name = "GetCategoryUnderParentName")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> GetCategoryUnderParentName([FromRoute]string Name, CancellationToken ct)
  {
    List<CategoryDto>? categories = new List<CategoryDto>();

    try
    {
      categories = await _categoryService.ListUnderAsync(Name);

      if (categories != null)
        return Ok(categories);
      else
        return NotFound("There are no category named " + Name);
    }
    catch (Exception e)
    {
      Log.Error(e.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
    }

  }

  [HttpGet("UnderParentID/{ParentCategoryID}", Name = "GetCategoriesUnder")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> GetCategoriesUnder([FromRoute] int ParentCategoryID, CancellationToken ct)
  {
    List<CategoryDto>? categories = new List<CategoryDto>();

    try
    {
      categories = await _categoryService.ListUnderAsync(ParentCategoryID);

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
