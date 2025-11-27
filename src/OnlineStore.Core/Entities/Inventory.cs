using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class Inventory
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime LastRestockedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
