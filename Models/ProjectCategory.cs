namespace ConstructionBidPortal.API.Models
{
    public class ProjectCategory
    {
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }
        
        // Navigation properties
        public Project Project { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
