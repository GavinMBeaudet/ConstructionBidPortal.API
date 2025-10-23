namespace ConstructionBidPortal.API.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ContractorId { get; set; }
        public string Proposal { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // "Submitted", "Awarded", "Rejected"
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        // Tennessee Official Bid Form fields
        public string ContractorName { get; set; } = string.Empty;
        public string ContractorAddress { get; set; } = string.Empty;
        public string ContractorCity { get; set; } = string.Empty;
        public string ContractorState { get; set; } = string.Empty;
        public string ContractorZip { get; set; } = string.Empty;
        public string ContractorLicense { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string OwnerAddress { get; set; } = string.Empty;
        public string OwnerCity { get; set; } = string.Empty;
        public string OwnerState { get; set; } = string.Empty;
        public string OwnerZip { get; set; } = string.Empty;
        public string LenderName { get; set; } = string.Empty;
        public string LenderAddress { get; set; } = string.Empty;
        public string LenderCity { get; set; } = string.Empty;
        public string LenderState { get; set; } = string.Empty;
        public string LenderZip { get; set; } = string.Empty;
        public string ProjectNumber { get; set; } = string.Empty;
        public string ProjectAddress { get; set; } = string.Empty;
        public string ProjectCity { get; set; } = string.Empty;
        public string ProjectState { get; set; } = string.Empty;
        public string ProjectZip { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public string OtherContractDocs { get; set; } = string.Empty;
        public string WorkInvolved { get; set; } = string.Empty;
        public string CommencementType { get; set; } = string.Empty;
        public string CommencementDays { get; set; } = string.Empty;
        public string CommencementOther { get; set; } = string.Empty;
        public string CompletionType { get; set; } = string.Empty;
        public string CompletionDays { get; set; } = string.Empty;
        public string CompletionOther { get; set; } = string.Empty;
        public decimal FinalContractPrice { get; set; }
        public string ProgressRetentionPercent { get; set; } = string.Empty;
        public string ProgressRetentionDays { get; set; } = string.Empty;
        public string FinalPaymentDays { get; set; } = string.Empty;
        public string TerminationDate { get; set; } = string.Empty;
        public string ProposalDate { get; set; } = string.Empty;
        public string WarrantyYears { get; set; } = string.Empty;
        public string AdditionalProvisions { get; set; } = string.Empty;
        public string ContractorSignaturesJson { get; set; } = string.Empty;
        public string OwnerSignaturesJson { get; set; } = string.Empty;

        // Navigation properties
        public Project Project { get; set; } = null!;
        public User Contractor { get; set; } = null!;
    }
}
