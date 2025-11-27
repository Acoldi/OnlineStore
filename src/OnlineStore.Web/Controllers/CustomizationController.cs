using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.CustomizationServices;

namespace OnlineStore.Web.Controllers;
[Route("api/Customization")]
[ApiController]
public class CustomizationController : ControllerBase
{
  private readonly ICustomizationsService _customizationsService;
  public CustomizationController(ICustomizationsService customizationsService)
  {
    _customizationsService = customizationsService;
  }

  [HttpGet("GetAllProductCustomizationOptions/{productID}")]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [Authorize]
  public async Task<ActionResult<List<CustomizationOptionDto>>> GetAllProductCustomizationOptions(int productID)
  {
    List<CustomizationOptionDto>? customizationOptions =
          await _customizationsService.ListCustomizationOptionsForProduct(productID);

    if (customizationOptions == null)
      return NotFound("No customization options found for this product.");

    return Ok(customizationOptions);
  }


}
