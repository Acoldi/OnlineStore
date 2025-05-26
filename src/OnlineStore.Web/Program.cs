using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.Interfaces.Products;
using OnlineStore.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using OnlineStore.Core.Interfaces.JWT;
using OnlineStore.Web.JWT;
using Microsoft.OpenApi.Models;
using OnlineStore.Core.Interfaces.Category;
using OnlineStore.Core.Interfaces;
using OnlineStore.Core.Interfaces.User;
using OnlineStore.Core.InterfacesAndServices.Category;
using OnlineStore.Core.InterfacesAndServices.User;
using OnlineStore.Core.InterfacesAndServices.Product;
using OnlineStore.Core.InterfacesAndServices.Order;
using OnlineStore.Core.InterfacesAndServices.OrderItem;
using Serilog.Core;
using OnlineStore.Core.InterfacesAndServices.ShoppingCart;

var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  //.WriteTo.File("Logs/log.txt")
  .WriteTo.Console()
  .WriteTo.File("Login/log.txt")
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
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"] ?? ""))
      };
      
      options.RequireHttpsMetadata = false;
      options.MapInboundClaims = true;
    });

builder.Services.AddAuthorization();

// Access the configuration
builder.Configuration.AddJsonFile("appsettings.json");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<OnlineStore.Infrastructure.Data.IConnectionFactory>
  ( _ => new ConnectionStringFactory(connectionString));

builder.Services.AddScoped<IDataAccess, DataAccessService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserSservice>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<ICartService, CartService>();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program { }
