using System;
using System.Collections.Generic;

namespace OnlineStore.Core.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public DateTime LastLogin { get; set; }

    public string Password { get; set; } = null!;

    public int? DefaultAddressId { get; set; }

    public bool IsActive { get; set; }

    public bool IsAdmin { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual Address? DefaultAddress { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ShoppingCart? ShoppingCart { get; set; }
}
