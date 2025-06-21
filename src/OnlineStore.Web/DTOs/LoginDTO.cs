
namespace OnlineStore.Web.DTOs;

public class LoginDTO
{
  public required Guid ID { get; set; }
  public required bool IsActive { get; set; }
  public required bool IsAdmin { get ; set; }
  public required string Password { get; set; }
  public required string email { get; set; }

}
