using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.InterfacesAndServices.JWT;
using OnlineStore.Core.InterfacesAndServices.UserServices;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Web.Controllers;
[Route("api/Login")]
[ApiController]
public class LoginController : ControllerBase
{
  IJWTService _jWTService;
  IUserService _userService;

  public LoginController(IJWTService jWTService, IUserService userService)
  {
    _jWTService = jWTService;
    _userService = userService;
  }

  [HttpGet("Login", Name = "Login")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Login(string email, string Password, CancellationToken ct)
  {
    User? user = await _userService.GetByEmailAsync(email, ct);

    if (user == null)
      throw new Exception("User should register first");

    LoginDTO? loginDTO = new LoginDTO()
    {
      ID = user.Id,
      email = user.EmailAddress,
      Password = user.Password,
      IsActive = user.IsActive,
      IsAdmin = user.IsAdmin
    };

    return Ok(_jWTService.GenerateJWT(loginDTO.ID.ToString()!, loginDTO.IsAdmin ? enRole.Admin : enRole.User));
  }


  [HttpGet("Register", Name = "Register")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register(string email, string password, CancellationToken ct)
  {
    User? user = await _userService.GetByEmailAsync(email, ct);

    if (user != null)
    {
      return BadRequest("This user is already registered with the email: " + email);
    }
    else
    {
      user = new()
      {
        EmailAddress = email,
        Password = password,
      };

      Guid NewUserID = await _userService.CreateAsync(user, ct);

      return Ok(_jWTService.GenerateJWT(NewUserID.ToString(), enRole.User));
    }
  }

}
