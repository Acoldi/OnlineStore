using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.UserServices;
using OnlineStore.Infrastructure.Data.Models;
using OnlineStore.Infrastructure.Data.RepositoriesImplementation;

namespace IntegrationTests;
public class SharedDataFixture
{
  public JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
  {
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
  };

}
