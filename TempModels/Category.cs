using System;
using System.Collections.Generic;

namespace ConstructionBidPortal.API.TempModels;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
