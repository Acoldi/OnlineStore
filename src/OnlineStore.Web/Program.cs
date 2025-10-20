using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineStore.Core.Interfaces.JWT;
using OnlineStore.Core.InterfacesAndServices;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.InterfacesAndServices.Payment;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Infrastructure.Data.RepositoriesImplementation;
using OnlineStore.Infrastructure.Data.RepositoriesImplementations;
using OnlineStore.Infrastructure.Options;
using OnlineStore.Infrastructure.PaymentServices;
using OnlineStore.Web.JWT;
using OnlineStore.Web.RepositoriesImplementations;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.File("Logs/log.txt")
  .WriteTo.Console()
  //.WriteTo.File("Login/log.txt")
  .CreateLogger();

logger.Information("Starting web host");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
  o.SwaggerDoc("Document", new OpenApiInfo()
  {
    Title = "Document",
    Version = "v1",
    Description = "Finally I'm settign this documentation up",

  });

  o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Description = "JWT Authorization header using Bearer scheme",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT"
  });

  o.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer",
        }
      },
      Array.Empty<string>()
    }
  });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"] ?? "")),
      };

      options.RequireHttpsMetadata = false;
      options.MapInboundClaims = false;
    });

builder.Services.AddAuthorization();


// Access the configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<OnlineStore.Infrastructure.Data.IConnectionFactory>
  (_ => new ConnectionStringFactory(connectionString));

//builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICustomizationRepo, CustomizationRepo>();
builder.Services.AddScoped<IPaytabPaymentService, PayTabsPaymentService>();
builder.Services.AddScoped<IZainCashPaymentService, ZainCashPaymentService>();
builder.Services.AddScoped<IAddressRepo, AddressRepo>();
builder.Services.AddScoped<ICityRepo, CityRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
builder.Services.AddScoped<IPaytabPaymentService, PayTabsPaymentService>();
builder.Services.AddScoped<IZainCashPaymentService, ZainCashPaymentService>();

// Using Options pattern
builder.Services.Configure<PayTabsOptions>(builder.Configuration.GetSection(PayTabsOptions.PayTabs));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/Document/swagger.json", "Document v1");
    options.RoutePrefix = "";
  });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();


// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program { }
