using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public DateTime? TurnedInAt { get; set; }

    public bool IsActive { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;
}
