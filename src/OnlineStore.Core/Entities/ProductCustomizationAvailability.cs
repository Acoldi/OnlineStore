using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

/// <summary>
/// Associatvie table
/// </summary>
public partial class ProductCustomizationAvailability
{
    public int CustomizationOptionId { get; set; }

    public int ProductId { get; set; }

    public virtual CustomizationOption CustomizationOption { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
