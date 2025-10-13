namespace ConstructionBidPortal.API.DTOs
{
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
        public string Status { get; set; } = "Submitted";
        public DateTime DateSubmitted { get; set; }
    }
}
