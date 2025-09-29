using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Core.Entities;
public class Address
{
  public int ID { get; set; }
  [Required]
  public Guid UserID { get; set; }
  [Required]
  public int CountryID { get; set; }
  public string? CountryName;
  [Required]
  public int CityID { get; set; }
  public string? CityName;

  [Required]
  public int StateID { get; set; }
  public string? StateName;

  public string? Area { get; set; }

  public Address(Guid userID, int countryID, int cityID, int stateID, string? area)
  {
    UserID = userID;
    CountryID = countryID;
    CityID = cityID;
    StateID = stateID;
    Area = area;
  }

  public Address() { }
}
