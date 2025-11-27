using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs.PaymentDtos;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Mappers;

namespace OnlineStore.Infrastructure.Mappers;
public class CustomerDetailsMapper : Mapper<User, CustomerDetailsDto>
{
  public static CustomerDetailsDto toDto(User Entity)
  {
    return new CustomerDetailsDto()
    {
      city = Entity.DefaultAddress?.City.Name ?? throw new ArgumentException("No user address."),
      country = Entity.DefaultAddress?.Country.Name ?? throw new ArgumentException("No user address."),
      email = Entity.EmailAddress,
      name = Entity.FirstName + " " + Entity.LastName,
      phone = Entity.PhoneNumber ?? throw new ArgumentException("No user address."),
      state = Entity.DefaultAddress.State?.Name ?? "",
      street1 = Entity.DefaultAddress.Street,
      zip = Entity.DefaultAddress.Zip.ToString(),
    };
  }

  public static User toEntity(CustomerDetailsDto dto)
  {
    string[] FullName = dto.name.Split(' ');
    User user = new User()
    {
      FirstName = FullName[0],
      LastName = FullName[1],
      EmailAddress = dto.email,
      PhoneNumber = dto.phone,
      Password = "",
    };

    return user;
  }
}
