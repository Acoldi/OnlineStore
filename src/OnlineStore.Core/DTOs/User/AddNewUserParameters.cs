namespace OnlineStore.Core.DTOs.User;

public class AddNewUserParameters
{
  public DateTime? LastLogin { get; set; } = DateTime.Now;
  public required string Password { get; set; }
  public bool IsActive { get; set; } = true;
  public bool IsAdmin { get; set; } = false;
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public required string EmailAddress { get; set; }
  public string? ImageUrl { get; set; }
  public string? PhoneNumber { get; set; }
}
