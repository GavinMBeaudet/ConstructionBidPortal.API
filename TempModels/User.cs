using System;
using System.Collections.Generic;

namespace ConstructionBidPortal.API.TempModels;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string LicenseNumber { get; set; } = null!;

    public string Title { get; set; } = null!;

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
