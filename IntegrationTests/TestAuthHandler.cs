using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IntegrationTests;
public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
  public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) 
    :base(options, logger, encoder)
  {
  }

  protected override Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    Claim[] claims = { new Claim(ClaimTypes.Name, "Test User"), new Claim(ClaimTypes.Role, "Admin") };
    ClaimsIdentity identity = new ClaimsIdentity(claims, "Test");
    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
    AuthenticationTicket ticket = new AuthenticationTicket(principal, "Test");

    AuthenticateResult result = AuthenticateResult.Success(ticket);

    return Task.FromResult(result);
  }
}
