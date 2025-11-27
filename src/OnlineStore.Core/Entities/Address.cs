namespace OnlineStore.Core.Entities;

public partial class Address
{
  public int Id { get; set; }

  public Guid UserId { get; set; }

  public int CountryId { get; set; }

  public int CityId { get; set; }

  public int? StateId { get; set; }

  public string Street { get; set; } = null!;

  public string? Zip { get; set; }

  public virtual City City { get; set; } = null!;

  public virtual Country Country { get; set; } = null!;

  public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

  public virtual State? State { get; set; }

  public virtual User User { get; set; } = null!;

  public virtual ICollection<User> Users { get; set; } = new List<User>();
}
