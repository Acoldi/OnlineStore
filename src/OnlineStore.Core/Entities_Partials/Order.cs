
namespace OnlineStore.Core.Entities;

/// <summary>
/// Pending=1,Shipping,Delivered,Cancelled
/// </summary>
public partial class Order
{
  public virtual Customer Customer { get; set; } = null!;
}
