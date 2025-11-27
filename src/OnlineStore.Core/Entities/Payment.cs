using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string TransactionId { get; set; } = null!;

    public decimal Amount { get; set; }

    /// <summary>
    ///   Pending = 1,
    ///   Processing = 2,
    ///   Authorized = 3,
    ///   Completed = 4,
    ///   Failed = 5,
    ///   Cancelled = 6,
    ///   Voided = 7,
    ///   Expired = 8,
    /// </summary>
    public short Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public short Method { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
