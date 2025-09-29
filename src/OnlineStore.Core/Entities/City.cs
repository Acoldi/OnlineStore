using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace OnlineStore.Core.Entities;
public  class City
{
  public int Id { get; set; }
  public required string Name { get; set; } 
  public int CountryID { get; set; }

  public City() { }
}
