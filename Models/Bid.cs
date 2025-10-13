namespace ConstructionBidPortal.API.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ContractorId { get; set; }
        public decimal BidAmount { get; set; }
        public int TimelineInDays { get; set; }
        public string Proposal { get; set; } = string.Empty;
        public string Status { get; set; } = "Submitted";
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
        
        // Navigation properties
        public Project Project { get; set; } = null!;
        public User Contractor { get; set; } = null!;
    }
}
