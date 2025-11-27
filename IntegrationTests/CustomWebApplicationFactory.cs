using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTests;
public class CustomWebApplicationFactory<Program> : WebApplicationFactory<Program> where Program : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(Services =>
    {
      Services.RemoveAll<IAuthenticationService>();
      Services.RemoveAll<IAuthenticationHandlerProvider>();

      Services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = "Test";
        options.DefaultChallengeScheme = "Test";
      })
      .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
    });
  }
}
