using System;
using System.Collections.Generic;

namespace ConstructionBidPortal.API.TempModels;

public partial class Project
{
    public int Id { get; set; }

    public int OwnerId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;

    public decimal Budget { get; set; }

    public DateTime BidDeadline { get; set; }

    public string Status { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string ProjectNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
