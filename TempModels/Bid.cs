using System;
using System.Collections.Generic;

namespace ConstructionBidPortal.API.TempModels;

public partial class Bid
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int ContractorId { get; set; }

    public string Proposal { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime DateSubmitted { get; set; }

    public string ContractorName { get; set; } = null!;

    public string ContractorAddress { get; set; } = null!;

    public string ContractorCity { get; set; } = null!;

    public string ContractorState { get; set; } = null!;

    public string ContractorZip { get; set; } = null!;

    public string ContractorLicense { get; set; } = null!;

    public string OwnerName { get; set; } = null!;

    public string OwnerAddress { get; set; } = null!;

    public string OwnerCity { get; set; } = null!;

    public string OwnerState { get; set; } = null!;

    public string OwnerZip { get; set; } = null!;

    public string LenderName { get; set; } = null!;

    public string LenderAddress { get; set; } = null!;

    public string LenderCity { get; set; } = null!;

    public string LenderState { get; set; } = null!;

    public string LenderZip { get; set; } = null!;

    public string ProjectNumber { get; set; } = null!;

    public string ProjectAddress { get; set; } = null!;

    public string ProjectCity { get; set; } = null!;

    public string ProjectState { get; set; } = null!;

    public string ProjectZip { get; set; } = null!;

    public string ProjectDescription { get; set; } = null!;

    public string OtherContractDocs { get; set; } = null!;

    public string WorkInvolved { get; set; } = null!;

    public string CommencementType { get; set; } = null!;

    public string CommencementDays { get; set; } = null!;

    public string CommencementOther { get; set; } = null!;

    public string CompletionType { get; set; } = null!;

    public string CompletionDays { get; set; } = null!;

    public string CompletionOther { get; set; } = null!;

    public decimal FinalContractPrice { get; set; }

    public string ProgressRetentionPercent { get; set; } = null!;

    public string ProgressRetentionDays { get; set; } = null!;

    public string FinalPaymentDays { get; set; } = null!;

    public DateOnly TerminationDate { get; set; }

    public DateOnly ProposalDate { get; set; }

    public string WarrantyYears { get; set; } = null!;

    public string AdditionalProvisions { get; set; } = null!;

    public string ContractorSignaturesJson { get; set; } = null!;

    public string OwnerSignaturesJson { get; set; } = null!;

    public virtual User Contractor { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
