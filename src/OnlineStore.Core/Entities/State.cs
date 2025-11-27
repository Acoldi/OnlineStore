using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class State
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string StateCode { get; set; } = null!;

    public int? CountryId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country? Country { get; set; }
}
