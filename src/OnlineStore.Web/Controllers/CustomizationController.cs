using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Web.Controllers;
[Route("api/CustomizationController")]
[ApiController]
public class CustomizationController : ControllerBase
{
  private readonly ICustomizationOptionRepo _customizationOptionRepo;
  public CustomizationController(ICustomizationOptionRepo customizationOptionRepo)
  {
    _customizationOptionRepo = customizationOptionRepo;
  }

  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<List<CustomizationOption>>> GetProductCustomizations(int productID)
  {
     List<CustomizationOption>? customizationOptions = await _customizationOptionRepo.GetProductCustomizationOptions();

    if (customizationOptions == null)
      return NotFound("No customization options found for this product.");

    return Ok(customizationOptions);
  }


}
