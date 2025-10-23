namespace ConstructionBidPortal.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty; // "Owner" or "Contractor"
        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Added for bid form accuracy
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty; // For contractors
        public string Title { get; set; } = string.Empty; // For owner signatures

        // Navigation properties
        public List<Project> OwnedProjects { get; set; } = new List<Project>();
        public List<Bid> ContractorBids { get; set; } = new List<Bid>();
    }
}