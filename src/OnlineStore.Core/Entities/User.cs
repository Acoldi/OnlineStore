using Azure.Identity;

namespace OnlineStore.Core.Entities;
public class User
{
  public Guid ID { get; set; }
  public DateTime LastLogin { get; set; }
  public required string Password { get; set; }
  public bool IsActive { get; set; } = true;
  public bool IsAdmin { get; set; } = false;
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public required string EmailAddress { get; set; }
  public string? ImageUrl { get; set; }
  public DateTime CreatedAt { get; set; }
  public string? PhoneNumber { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public int? DefaultAddressID { get; set; }

  public User()
  {  }

  public User(bool isAdmin,
              string? firstName, string? lastName, string emailAddress, string? imageUrl, string? phoneNumber,
              string password)
  {
    LastLogin = DateTime.Now;
    CreatedAt = DateTime.Now;
    UpdatedAt = DateTime.Now;
    IsActive = true;
    this.IsAdmin = isAdmin;
    FirstName = firstName;
    LastName = lastName;
    EmailAddress = emailAddress;
    ImageUrl = imageUrl;
    PhoneNumber = phoneNumber;
    Password = password;
  }
}
