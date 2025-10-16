namespace ConstructionBidPortal.API.DTOs
{
    public class BidDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ContractorId { get; set; }
        public decimal BidAmount { get; set; }
        public int TimelineInDays { get; set; }
        public string Proposal { get; set; } = string.Empty;
        public string Status { get; set; } = "Submitted";
        public DateTime DateSubmitted { get; set; }
        public ProjectSummaryDto Project { get; set; } = null!;
        public ContractorSummaryDto Contractor { get; set; } = null!;
    }

    public class ProjectSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Budget { get; set; }
    }

    public class ContractorSummaryDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class CreateBidDto
    {
        public int ProjectId { get; set; }
        public int ContractorId { get; set; }
        public decimal BidAmount { get; set; }
        public int TimelineInDays { get; set; }
        public string Proposal { get; set; } = string.Empty;
    }

    public class UpdateBidDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ContractorId { get; set; }
        public decimal BidAmount { get; set; }
        public int TimelineInDays { get; set; }
        public string Proposal { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; }
    }
}
