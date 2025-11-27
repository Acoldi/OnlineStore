using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.InterfacesAndServices.JWT;
public interface IJWTService
{
  public string GenerateJWT(string userID, enRole Role);
}
