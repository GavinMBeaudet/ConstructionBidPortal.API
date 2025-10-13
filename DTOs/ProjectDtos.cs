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
    }
}
