using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Web.Controllers;
[Route("api/Address")]
[ApiController]
public class AddressController : ControllerBase
{
  IAddressRepo _addressRepo;
  public AddressController(IAddressRepo addressRepo)
  {
    _addressRepo = addressRepo;
  }

  [Authorize(Roles = "Admin")]
  [HttpPost("create", Name = "CreateAddress")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> CreateAddress(Address address, CancellationToken ct)
  {
    address.CityID = await _addressRepo.CreateAsync(address, ct);

    return NoContent();
  }
}
