using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class Customization
{
    public int OrderItemId { get; set; }

    public int CustomizationChoiceId { get; set; }

    public virtual CustomizationChoice CustomizationChoice { get; set; } = null!;

    public virtual OrderItem OrderItem { get; set; } = null!;
}
