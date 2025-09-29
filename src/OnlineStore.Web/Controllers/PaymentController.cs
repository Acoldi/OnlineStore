using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.Payment;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Web.Controllers;
[Route("api/Payment")]
[ApiController]
public class PaymentController : ControllerBase
{
  private readonly IZainCashPaymentService _ZinPaymentsService;
  private readonly IPaytabPaymentService _PTS_PaymentService;
  private readonly IPaymentRepo _paymentRepo;
  public PaymentController(IPaytabPaymentService paytabPaymentService ,IPaymentRepo payment,IZainCashPaymentService paymentService)
  {
    _PTS_PaymentService = paytabPaymentService;
    _paymentRepo = payment;
    _ZinPaymentsService = paymentService;
  }


  //[Authorize]
  //[HttpGet("ZainCashCallback", Name = "ZainCashCallback")]
  //[ProducesResponseType(StatusCodes.Status200OK)]
  //[ProducesResponseType(StatusCodes.Status400BadRequest)]
  //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
  


  [Authorize(Roles = "Admin")]
  [HttpGet("ZainCashCallback", Name = "ZainCashCallback")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ZainCashCallback(string token)
  {
    if (string.IsNullOrEmpty(token))
      return BadRequest("Missing token.");

    Dictionary<string, string> result = await _ZinPaymentsService.GetZaincashCallBackResults(token);

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

  [Authorize]
  [HttpGet("PaymentReturnPage", Name = "PayBackReturnPage")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> PayTabCallBack([FromHeader(Name = "signature")]string Signature, PayTabCallBackReturnResponse returnResultPT)
  {
    // Validating the request
    if (_PTS_PaymentService.ValidateCallBackPayloadSignature(returnResultPT, Signature))
      return BadRequest("Unvalid token!");

    Payment payment = new Payment()
    {
      Amount = returnResultPT.Amount,
      CreatedAt = DateTime.UtcNow,
      Method = Core.Enums.enPaymentMethod.MasterVisa,
      OrderID = int.Parse(returnResultPT.OrderID),
      Status = Core.Enums.enPaymentStatus.Pending,
      TransactionID = returnResultPT.TransferID,
      UpdatedAt = DateTime.UtcNow,
    };

    payment.ID = await _paymentRepo.CreateAsync(payment);

    switch (returnResultPT.paymentResult.ResponseStatus)
    {
      case "A":
        payment.Status = Core.Enums.enPaymentStatus.Authorized;
        Log.Logger.Information("Paytabs - Payment info: " + "A; " + returnResultPT.paymentResult.ResponseMessage);
        break;
      case "H":
        payment.Status = Core.Enums.enPaymentStatus.Authorized;
        Log.Logger.Information("Paytabs - Payment info: " + "H; " + returnResultPT.paymentResult.ResponseMessage);
        break;
      case "P":
        payment.Status = Core.Enums.enPaymentStatus.Pending;
        Log.Logger.Information("Paytabs - Payment info: " + "P; " + returnResultPT.paymentResult.ResponseMessage);
        break;
      case "V":
        payment.Status = Core.Enums.enPaymentStatus.Voided;
        Log.Logger.Information("Paytabs - Payment info: " + "V; " + returnResultPT.paymentResult.ResponseMessage);
        break;
      case "E":
        payment.Status = Core.Enums.enPaymentStatus.Failed;
        Log.Logger.Information("Paytabs - Payment info: " + "E; " + returnResultPT.paymentResult.ResponseMessage);
        break;
      case "D":
        payment.Status = Core.Enums.enPaymentStatus.Cancelled;
        Log.Logger.Information("Paytabs - Payment info: " + "D; " + returnResultPT.paymentResult.ResponseMessage);
        break;
      case "X":
        payment.Status = Core.Enums.enPaymentStatus.Expired;
        Log.Logger.Information("Paytabs - Payment info: " + "X; " + returnResultPT.paymentResult.ResponseMessage);
        break;
    }

    if (await _paymentRepo.UpdateAsync(payment))
      return Ok();
    else return BadRequest();
  }
}
