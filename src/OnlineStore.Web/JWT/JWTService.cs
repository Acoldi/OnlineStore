using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.Enums;
using OnlineStore.Core.InterfacesAndServices.JWT;

namespace OnlineStore.Web.JWT;

public class JWTService : IJWTService
{
  private IConfiguration _configuration;

  public JWTService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public string GenerateJWT(string userID, enRole Role)
  {
    JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

    SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"] ?? 
                                    throw new ConfigurationErrorsException("jwt security key is not provided"))),
    SecurityAlgorithms.HmacSha256);


    Claim[] claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, userID.ToString()),
        new Claim("role", Role.ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString())
    };

    JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
        issuer: _configuration["JwtSettings:issuer"],
        audience: _configuration["JwtSettings:audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(2),
        signingCredentials: credentials
    );

    return _jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
  }
}
