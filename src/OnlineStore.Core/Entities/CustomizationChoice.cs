using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class CustomizationChoice
{
    public int Id { get; set; }

    public int OptionId { get; set; }

    public string Value { get; set; } = null!;

    public virtual CustomizationOption Option { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
