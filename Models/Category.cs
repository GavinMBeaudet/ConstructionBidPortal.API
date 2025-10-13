namespace ConstructionBidPortal.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // Navigation property
        public List<ProjectCategory> ProjectCategories { get; set; } = new List<ProjectCategory>();
    }
}
