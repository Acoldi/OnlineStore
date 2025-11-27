using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class CartItem
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public Guid UserId { get; set; }
}
