using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class Image
{
    public int Id { get; set; }

    public string? ImagePath { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
