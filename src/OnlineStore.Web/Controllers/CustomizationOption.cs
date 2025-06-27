using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Web.Controllers;
[Route("api/Customization")]
[ApiController]
public class CustomizationOption : ControllerBase
{
  private readonly ICustomizationChoiceRepo _customizationRepo;
  private readonly ICustomizationChoiceRepo _customizationChoiceRepo;
  public CustomizationOption(ICustomizationChoiceRepo customizationChoiceRepo1, ICustomizationChoiceRepo customizationChoiceRepo)
  {
    _customizationRepo = customizationChoiceRepo;
    _customizationChoiceRepo = customizationChoiceRepo1;
  }

  [HttpPost]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> CreateCustomizationChoice(CustomizationChoice customizationChoice, CancellationToken ct)
  {
    try
    {
      await _customizationChoiceRepo.CreateAsync(customizationChoice, ct);
      return NoContent();
      
    }
    catch
    {
      return BadRequest("Unvalid CustomizationChoice object");
    }
  }

}
