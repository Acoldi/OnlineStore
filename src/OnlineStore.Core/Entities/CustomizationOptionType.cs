using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class CustomizationOptionType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<CustomizationOption> CustomizationOptions { get; set; } = new List<CustomizationOption>();
}
