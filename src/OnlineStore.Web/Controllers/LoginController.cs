using System.Data;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.Interfaces.JWT;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Web.Controllers;
[Route("api/Login")]
[ApiController]
public class LoginController : ControllerBase
{
  IJWTService _jWTService;
  IUserRepo _userRepo;

  public LoginController(IJWTService jWTService, IUserRepo userRepo)
  {
    _jWTService = jWTService;
    _userRepo = userRepo;
  }

  [HttpGet("Login", Name ="Login")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Login(string email, string Password, CancellationToken ct)
  {
    OnlineStore.Core.Entities.User? user = await _userRepo.GetByEmail(email, ct);

    if (user == null)
      throw new Exception("User should register first");

    LoginDTO? loginDTO = new LoginDTO() { ID = user.ID, email = user.EmailAddress, Password = user.Password, 
      IsActive = user.IsActive, IsAdmin = user.IsAdmin};

    return Ok(_jWTService.GetJWToken(loginDTO.ID.ToString()!, loginDTO.IsAdmin ? enRole.Admin : enRole.User));
  }


  [HttpGet("Register", Name = "Register")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register(string email, string password, CancellationToken ct)
  {
    Core.Entities.User? user = await _userRepo.GetByEmail(email, ct);

    if (user != null)
    {
      return BadRequest("This user is already registered with the email: " + email);
    }
    else
    {
      user = new Core.Entities.User()
      {
        EmailAddress = email,
        Password = password,
      };
      //                         USer id is guid
      Guid NewUserID = await _userRepo.CreateAsync(user, ct);

      return Ok(_jWTService.GetJWToken(NewUserID.ToString(), enRole.User));
    }
  }

}
