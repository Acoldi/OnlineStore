using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Entities;
public class Address
{
  [Required]
  public Guid UserID { get; set; }
  [Required]
  public int CountryID { get; set; }
  [Required]
  public int CityID { get; set; }
  [Required]
  public required string Area { get; set; }

  public Address(Guid userID, int countryID, int cityID, string area)
  {
    UserID = userID;
    CountryID = countryID;
    CityID = cityID;
    Area = area;
  }

  public Address() { }
}
