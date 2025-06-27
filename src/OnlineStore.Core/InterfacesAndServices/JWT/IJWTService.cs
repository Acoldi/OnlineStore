using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.Interfaces.JWT;
public interface IJWTService
{
  public string GenerateJWT(string userID, enRole Role);
  public ClaimsPrincipal ValidateJWT(string JWT);
}
