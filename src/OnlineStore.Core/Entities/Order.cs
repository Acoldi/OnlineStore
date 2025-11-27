using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

/// <summary>
/// Pending=1,Shipping,Delivered,Cancelled
/// </summary>
public partial class Order
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public int ShippingAddressId { get; set; }

    public int CustomerId { get; set; }

    public decimal TotalAmount { get; set; }

    /// <summary>
    ///   Pending = 1,
    ///   Shipping,
    ///   Delivered,
    ///   Cancelled
    /// </summary>
    public short OrderStatus { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Address ShippingAddress { get; set; } = null!;

    public virtual Customer ShippingAddressNavigation { get; set; } = null!;
}
