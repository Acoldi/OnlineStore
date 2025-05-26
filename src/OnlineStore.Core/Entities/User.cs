using System.Net.Mail;

namespace OnlineStore.Core.Entities;
public class User
{
  public Guid ID { get; set; }
  public DateTime LastLogin { get; set; }
  public string Password { get; set; }
  public bool IsActive { get; set; } = true;
  public bool IsAdmin { get; set; } = false;
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? EmailAddress { get; set; }
  public string? ImageUrl { get; set; }
  public DateTime CreatedAt { get; set; }
  public string? PhoneNumber { get; set; }
  public DateTime UpdatedAt { get; set; }

  public User()
  {
    this.ID = default;
    LastLogin = default;
    CreatedAt = default;
    UpdatedAt = default;
    FirstName = string.Empty;
    LastName = string.Empty;
    EmailAddress = string.Empty;
    ImageUrl = string.Empty;
    PhoneNumber = string.Empty;
    Password = string.Empty;
  }

  public User(Guid ID, DateTime lastLogin, bool isAdmin, bool isActive,
              string? firstName, string? lastName, string? emailAddress, string? imageUrl, string? phoneNumber,
              string password)
  {
    this.ID = ID;
    LastLogin = lastLogin;
    CreatedAt = DateTime.Now;
    UpdatedAt = DateTime.Now;
    IsActive = isAdmin;
    FirstName = firstName;
    LastName = lastName;
    EmailAddress = emailAddress;
    ImageUrl = imageUrl;
    PhoneNumber = phoneNumber;
    Password = password;
  }
}
