namespace ConstructionBidPortal.API.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public DateTime BidDeadline { get; set; }
        public string Status { get; set; } = "Open";
        public DateTime DateCreated { get; set; } = DateTime.Now;
        
        // Navigation properties
        public User Owner { get; set; } = null!;
        public List<Bid> Bids { get; set; } = new List<Bid>();
        public List<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();
    }
}
