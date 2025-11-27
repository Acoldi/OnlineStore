using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class Video
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string VideoPath { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
