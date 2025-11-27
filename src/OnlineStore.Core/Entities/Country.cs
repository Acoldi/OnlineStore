using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string DialCode { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public string Isoalpha2 { get; set; } = null!;

    public string IsocurrencyCode { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
