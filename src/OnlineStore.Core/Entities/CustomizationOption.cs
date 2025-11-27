using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class CustomizationOption
{
    public int Id { get; set; }

    public string Label { get; set; } = null!;

    public int TypeId { get; set; }

    public int ProductId { get; set; }

    public decimal AdditionalCost { get; set; }

    public virtual ICollection<CustomizationChoice> CustomizationChoices { get; set; } = new List<CustomizationChoice>();

    public virtual CustomizationOptionType Type { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
