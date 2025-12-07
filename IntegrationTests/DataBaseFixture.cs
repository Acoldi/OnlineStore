using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using OnlineStore.Core.Entities;
using OnlineStore.Infrastructure.Data.Models;

namespace IntegrationTests;
public class DataBaseFixture
{
  IConfigurationRoot _configuration;

  public User TestUser { get; set; }

  public DataBaseFixture()
  {
    _configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

    using EStoreSystemContext context = CreateContext();
    //context.Database.EnsureDeleted();
    //context.Database.EnsureCreated();

    // Create a user for all tests. For every test, it can be changed 
    TestUser = context.Users.Where(u => u.IsAdmin == true).First();
  }

  public EStoreSystemContext CreateContext()
  {
    return new EStoreSystemContext(
      new DbContextOptionsBuilder<EStoreSystemContext>()
      .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
      .Options
    );
  }
}
