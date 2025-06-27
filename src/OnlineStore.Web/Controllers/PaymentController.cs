using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.InterfacesAndServices.Payment;

namespace OnlineStore.Web.Controllers;
[Route("api/Payment")]
[ApiController]
public class PaymentController : ControllerBase
{
  private readonly IPaymentService _PaymentsService;
  public PaymentController(IPaymentService paymentService)
  {
    _PaymentsService = paymentService;
  }


  //[Authorize]
  //[HttpGet("ZainCashCallback", Name = "ZainCashCallback")]
  //[ProducesResponseType(StatusCodes.Status200OK)]
  //[ProducesResponseType(StatusCodes.Status400BadRequest)]
  //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
  


  [Authorize]
  [HttpGet("ZainCashCallback", Name = "ZainCashCallback")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ZainCashCallback(string token)
  {
    if (string.IsNullOrEmpty(token))
      return BadRequest("Missing token.");

    Dictionary<string, string> result = await _PaymentsService.GetZaincashCallBackResults(token);

    if (result["status"] == "success" || result["status"] == "completed")
      return Ok("Successfull Transaction");
    else if (result["status"] == "failed")
      return BadRequest(result["msg"]);
    else if (result["status"] == "pending")
    {
      return Ok("Transaction is still being processed...\nTransaction ID: " + result["id"]);
    }
    else
      return StatusCode(StatusCodes.Status500InternalServerError);
  }

}
