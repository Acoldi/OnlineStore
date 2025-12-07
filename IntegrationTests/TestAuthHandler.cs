using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IntegrationTests;
public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
  DataBaseFixture _dataBaseFixture;
  public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
    : base(options, logger, encoder)
  {
    _dataBaseFixture = new DataBaseFixture();
  }

  protected override Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    Claim[] claims = { new Claim(ClaimTypes.NameIdentifier, _dataBaseFixture.TestUser.Id.ToString()),
      new Claim(ClaimTypes.Name, "Test User"), new Claim(ClaimTypes.Role, "Admin")};

    ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
    AuthenticationTicket ticket = new AuthenticationTicket(principal, "Test");

    AuthenticateResult result = AuthenticateResult.Success(ticket);

    return Task.FromResult(result);
  }
}
