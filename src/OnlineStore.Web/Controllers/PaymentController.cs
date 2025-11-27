using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Enums;
using OnlineStore.Core.InterfacesAndServices.PaymentServices;
using OnlineStore.Infrastructure.Options;
using OnlineStore.Web.DTOs;
using OnlineStore.Web.Mappers;

namespace OnlineStore.Web.Controllers;
[Route("api/Payment")]
[ApiController]
public class PaymentController : ControllerBase
{
  private readonly ZainCashOptions _zainCashOptions;
  public PaymentController(ZainCashOptions zainCashOptions)
  {
    _zainCashOptions = zainCashOptions;
  }

  [HttpPost("ZainCashCallback", Name = "ZainCashCallback")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ZainCashCallback([FromKeyedServices("ZainCash")] IPaymentService zainCashPaymentService,
    string token)
  {
    if (string.IsNullOrEmpty(token))
      return BadRequest("Missing token.");

    try
    {

      ZainCashCallBadkDto? zainCashCallBadkDto = JsonSerializer.Deserialize<ZainCashCallBadkDto>
        (Jose.JWT.Decode(token, System.Text.Encoding.UTF8.GetBytes(_zainCashOptions.secret)));

      if (zainCashCallBadkDto == null)
        return BadRequest();

      enPaymentStatus result = await zainCashPaymentService.ProcessPaymentCallBack(ZainCashCallBackMapper.
        toPaymentDto(zainCashCallBadkDto, _zainCashOptions));

      return Ok(result);

    }
    catch (Exception ex)
    {
      Log.Logger.Error(ex, ex.Message);
      return BadRequest("Invalid token");
    }
  }

  [HttpPost("PayTabCallBack", Name = "PayTabCallBack")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> PayTabCallBack([FromKeyedServices("PayTab")] IPaymentService paytabPaymentService,
    [FromHeader(Name = "signature")] string Signature,
    PayTabCallBackDto callbackData)
  {
    using (StreamReader sr = new StreamReader(HttpContext.Request.Body))
    {
      HttpContext.Request.Body.Position = 0;

      string bodyRow = sr.ReadToEnd();

      if (await paytabPaymentService.ValidateCallBack(bodyRow, Signature))
      {

        Log.Logger.Information(callbackData.PaymentResult.ToString());

        await paytabPaymentService.ProcessPaymentCallBack(PayTabCallBackMapper.PaytabToPaymentDto(callbackData));

        return Ok();
      }
      else
        return Unauthorized("Failed to validate request.");
    }
  }
}
