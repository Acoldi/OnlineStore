using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class OrderItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int? OrderId { get; set; }

    public int Quantity { get; set; }

    public int? ShoppingCartId { get; set; }

    public decimal Price { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ShoppingCart? ShoppingCart { get; set; }

    public virtual ICollection<CustomizationChoice> CustomizationChoices { get; set; } = new List<CustomizationChoice>();
}
