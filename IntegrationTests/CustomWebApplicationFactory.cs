using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTests;
public class CustomWebApplicationFactory<Program> : WebApplicationFactory<Program> where Program : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureAppConfiguration(c =>
    {
      c.AddUserSecrets<Program>();
    });

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

    builder.UseEnvironment("Development");
  }


}
