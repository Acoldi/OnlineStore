using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class ShoppingCart
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
