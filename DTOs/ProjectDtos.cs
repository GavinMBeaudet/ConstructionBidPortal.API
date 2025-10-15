using System;
using System.Collections.Generic;

namespace ConstructionBidPortal.API.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime BidDeadline { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public int OwnerId { get; set; }
        public OwnerDto Owner { get; set; } = null!;
        public List<CategoryDto> Categories { get; set; } = new();
    }

    public class OwnerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
namespace ConstructionBidPortal.API.DTOs
{
    public class CreateProjectDto
    {
        public int OwnerId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime BidDeadline { get; set; }
        public string Status { get; set; } = "Open";
        public List<int> CategoryIds { get; set; } = new List<int>();
    }

    public class UpdateProjectDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime BidDeadline { get; set; }
        public string Status { get; set; } = "Open";
        public DateTime DateCreated { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
