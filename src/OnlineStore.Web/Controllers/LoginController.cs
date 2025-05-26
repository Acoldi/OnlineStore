using System.Data;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.Interfaces.JWT;
using OnlineStore.Core.Interfaces.User;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Web.Controllers;
[Route("api/Login")]
[ApiController]
public class LoginController : ControllerBase
{
  IDataAccess _DAccess;
  IJWTService _jWTService;
  IUserService _userService;

  public LoginController(IDataAccess DA, IJWTService jWTService, IUserService userService)
  {
    _DAccess = DA;
    _jWTService = jWTService;
    _userService = userService;
  }

  [HttpGet("Login", Name ="Login")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Login(string email, string Password, CancellationToken ct)
  {
    LoginDTO? loginDTO = await _DAccess.LoadSingleAsync<LoginDTO>("SP_GetUserByEmail", CommandType.StoredProcedure, ct, new { email });

    if (loginDTO != null)
      return Ok(_jWTService.GetJWToken(loginDTO.ID.ToString()!, loginDTO.IsAdmin ? enRole.Admin : enRole.User));
    else
      return BadRequest("User Need to register first");
  }


  [HttpGet("Register", Name = "Register")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register(string email, string password, CancellationToken ct)
  {
    LoginDTO? UserLoginDTO = await _DAccess.LoadSingleAsync<LoginDTO>("SP_GetUserByEmail", CommandType.StoredProcedure, ct, email);

    if (UserLoginDTO != null)
    {
      return BadRequest("There is already a registered email: " + email);
    }
    else
    {
      int NewUserID = await _userService.Create(new AddNewUserParameters { EmailAddress = email, Password = password }, ct);

      if (NewUserID > 0)
        return Ok(_jWTService.GetJWToken(NewUserID.ToString(), enRole.User));
      else
        return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }

}
