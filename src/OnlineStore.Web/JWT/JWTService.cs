using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ExceptionServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.Enums;
using OnlineStore.Core.Interfaces.JWT;

namespace OnlineStore.Web.JWT;

public class JWTService : IJWTService
{
  static private JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
  private IConfiguration _configuration;

  public JWTService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public string GenerateJWT(string userID, enRole Role)
  {
    if (_configuration["JwtSettings:SecurityKey"] == null) throw new ConfigurationErrorsException("SecurityKey can't be null");

      SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(
                                      Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"] ?? "")),
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
        expires: DateTime.Now.AddYears(1),
        signingCredentials: credentials
    );

    return jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
  }
}
