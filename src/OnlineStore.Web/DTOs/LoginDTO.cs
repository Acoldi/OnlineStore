using OnlineStore.Core.Interfaces.DTOs;

namespace OnlineStore.Web.DTOs;

public class LoginDTO
{
  public Guid? ID { get; set; }
  public bool IsActive { get; set; }
  public bool IsAdmin { get ; set; }
  public string Password { get; set; }
  public string email { get; set; }

  public LoginDTO()
  {
    ID = null;
    Password = "";
    email = "";
  }
  public LoginDTO(Guid ID, string email, string Password, bool isAdmin, bool IsActive)
  {
    this.ID = ID;
    this.IsActive = isAdmin;
    this.email = email;
    this.Password = Password;
    this.IsAdmin = IsAdmin;
  }
}
