using Microsoft.EntityFrameworkCore;
using ConstructionBidPortal.API.Data;
using ConstructionBidPortal.API.Models;
using ConstructionBidPortal.API.DTOs;

namespace ConstructionBidPortal.API.Endpoints;

public static class BidsEndpoints
{
    public static void MapBidsEndpoints(this WebApplication app)
    {
        // GET /api/bids - Get all bids with optional filtering
        app.MapGet("/api/bids", async (
            BidPortalContext context,
            int? projectId,
            int? contractorId) =>
        {
            var query = context.Bids
                .Include(b => b.Project)
                .Include(b => b.Contractor)
                .AsQueryable();

            if (projectId.HasValue)
            {
                query = query.Where(b => b.ProjectId == projectId.Value);
            }

            if (contractorId.HasValue)
            {
                query = query.Where(b => b.ContractorId == contractorId.Value);
            }

            var bids = await query.ToListAsync();
            var bidDtos = bids.Select(b => new BidDto
            {
                Id = b.Id,
                ProjectId = b.ProjectId,
                ContractorId = b.ContractorId,
                Proposal = b.Proposal,
                Status = b.Status,
                DateSubmitted = b.DateSubmitted,
                Project = new ProjectSummaryDto
                {
                    Id = b.Project.Id,
                    Title = b.Project.Title,
                    Location = b.Project.Location,
                    Budget = b.Project.Budget
                },
                Contractor = new ContractorSummaryDto
                {
                    Id = b.Contractor.Id,
                    FirstName = b.Contractor.FirstName,
                    LastName = b.Contractor.LastName,
                    Email = b.Contractor.Email
                },
                ContractorName = b.ContractorName,
                ContractorAddress = b.ContractorAddress,
                ContractorCity = b.ContractorCity,
                ContractorState = b.ContractorState,
                ContractorZip = b.ContractorZip,
                ContractorLicense = b.ContractorLicense,
                OwnerName = b.OwnerName,
                OwnerAddress = b.OwnerAddress,
                OwnerCity = b.OwnerCity,
                OwnerState = b.OwnerState,
                OwnerZip = b.OwnerZip,
                LenderName = b.LenderName,
                LenderAddress = b.LenderAddress,
                LenderCity = b.LenderCity,
                LenderState = b.LenderState,
                LenderZip = b.LenderZip,
                ProjectNumber = b.ProjectNumber,
                ProjectAddress = b.ProjectAddress,
                ProjectCity = b.ProjectCity,
                ProjectState = b.ProjectState,
                ProjectZip = b.ProjectZip,
                ProjectDescription = b.ProjectDescription,
                OtherContractDocs = b.OtherContractDocs,
                WorkInvolved = b.WorkInvolved,
                CommencementType = b.CommencementType,
                CommencementDays = b.CommencementDays,
                CommencementOther = b.CommencementOther,
                CompletionType = b.CompletionType,
                CompletionDays = b.CompletionDays,
                CompletionOther = b.CompletionOther,
                FinalContractPrice = b.FinalContractPrice,
                ProgressRetentionPercent = b.ProgressRetentionPercent,
                ProgressRetentionDays = b.ProgressRetentionDays,
                FinalPaymentDays = b.FinalPaymentDays,
                TerminationDate = b.TerminationDate,
                ProposalDate = b.ProposalDate,
                WarrantyYears = b.WarrantyYears,
                AdditionalProvisions = b.AdditionalProvisions,
                ContractorSignatures = string.IsNullOrEmpty(b.ContractorSignaturesJson)
    ? new List<SignatureDto>()
    : System.Text.Json.JsonSerializer.Deserialize<List<SignatureDto>>(b.ContractorSignaturesJson) ?? new List<SignatureDto>(),

                OwnerSignatures = string.IsNullOrEmpty(b.OwnerSignaturesJson)
    ? new List<SignatureDto>()
    : System.Text.Json.JsonSerializer.Deserialize<List<SignatureDto>>(b.OwnerSignaturesJson) ?? new List<SignatureDto>()
            }).ToList();
            return Results.Ok(bidDtos);
        });

        // GET /api/bids/{id} - Get a specific bid by ID
        app.MapGet("/api/bids/{id}", async (
            int id,
            BidPortalContext context) =>
        {
            var bid = await context.Bids
                .Include(b => b.Project)
                .Include(b => b.Contractor)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bid == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(bid);
        });

        // POST /api/bids - Create a new bid
        app.MapPost("/api/bids", async (
            CreateBidDto bidDto,
            BidPortalContext context) =>
        {
            // Backend validation for required Tennessee bid form fields (OwnerSignatures not required on creation)
            if (string.IsNullOrWhiteSpace(bidDto.ContractorName) ||
                string.IsNullOrWhiteSpace(bidDto.OwnerName) ||
                bidDto.FinalContractPrice <= 0 ||
                bidDto.ContractorSignatures == null || bidDto.ContractorSignatures.Count == 0)
            {
                return Results.BadRequest("Missing required Tennessee bid form fields: Contractor Name, Owner Name, Final Contract Price, Contractor Signatures.");
            }

            var bid = new Bid
            {
                ProjectId = bidDto.ProjectId,
                ContractorId = bidDto.ContractorId,
                Proposal = bidDto.Proposal,
                Status = "Pending",
                DateSubmitted = DateTime.Now,
                ContractorName = bidDto.ContractorName,
                ContractorAddress = bidDto.ContractorAddress,
                ContractorCity = bidDto.ContractorCity,
                ContractorState = bidDto.ContractorState,
                ContractorZip = bidDto.ContractorZip,
                ContractorLicense = bidDto.ContractorLicense,
                OwnerName = bidDto.OwnerName,
                OwnerAddress = bidDto.OwnerAddress,
                OwnerCity = bidDto.OwnerCity,
                OwnerState = bidDto.OwnerState,
                OwnerZip = bidDto.OwnerZip,
                LenderName = bidDto.LenderName,
                LenderAddress = bidDto.LenderAddress,
                LenderCity = bidDto.LenderCity,
                LenderState = bidDto.LenderState,
                LenderZip = bidDto.LenderZip,
                ProjectNumber = bidDto.ProjectNumber,
                ProjectAddress = bidDto.ProjectAddress,
                ProjectCity = bidDto.ProjectCity,
                ProjectState = bidDto.ProjectState,
                ProjectZip = bidDto.ProjectZip,
                ProjectDescription = bidDto.ProjectDescription,
                OtherContractDocs = bidDto.OtherContractDocs,
                WorkInvolved = bidDto.WorkInvolved,
                CommencementType = bidDto.CommencementType,
                CommencementDays = bidDto.CommencementDays,
                CommencementOther = bidDto.CommencementOther,
                CompletionType = bidDto.CompletionType,
                CompletionDays = bidDto.CompletionDays,
                CompletionOther = bidDto.CompletionOther,
                FinalContractPrice = bidDto.FinalContractPrice,
                ProgressRetentionPercent = bidDto.ProgressRetentionPercent,
                ProgressRetentionDays = bidDto.ProgressRetentionDays,
                FinalPaymentDays = bidDto.FinalPaymentDays,
                TerminationDate = bidDto.TerminationDate,
                ProposalDate = bidDto.ProposalDate,
                WarrantyYears = bidDto.WarrantyYears,
                AdditionalProvisions = bidDto.AdditionalProvisions,
                ContractorSignaturesJson = bidDto.ContractorSignatures != null ? System.Text.Json.JsonSerializer.Serialize(bidDto.ContractorSignatures) : "",
                OwnerSignaturesJson = bidDto.OwnerSignatures != null ? System.Text.Json.JsonSerializer.Serialize(bidDto.OwnerSignatures) : ""
            };

            context.Bids.Add(bid);
            await context.SaveChangesAsync();

            return Results.Created($"/api/bids/{bid.Id}", bid);
        });

        // PUT /api/bids/{id} - Update an existing bid
        app.MapPut("/api/bids/{id}", async (
            int id,
            UpdateBidDto bidDto,
            BidPortalContext context) =>
        {
            if (id != bidDto.Id)
            {
                return Results.BadRequest("ID mismatch");
            }

            var existingBid = await context.Bids.FindAsync(id);
            if (existingBid == null)
            {
                return Results.NotFound();
            }

            // Authorization: Only the contractor who created the bid can update it
            if (existingBid.ContractorId != bidDto.ContractorId)
            {
                return Results.Problem(
                    statusCode: StatusCodes.Status403Forbidden,
                    detail: "You can only edit your own bids.");
            }

            existingBid.Proposal = bidDto.Proposal;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await context.Bids.AnyAsync(e => e.Id == id);
                if (!exists)
                {
                    return Results.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Results.NoContent();
        });

        // DELETE /api/bids/{id} - Delete a bid
        app.MapDelete("/api/bids/{id}", async (
            int id,
            int userId,
            BidPortalContext context) =>
        {
            var bid = await context.Bids.FindAsync(id);
            if (bid == null)
            {
                return Results.NotFound();
            }

            // Authorization: Only the contractor who created the bid can delete it
            if (bid.ContractorId != userId)
            {
                return Results.Problem(
                    statusCode: StatusCodes.Status403Forbidden,
                    detail: "You can only delete your own bids.");
            }

            context.Bids.Remove(bid);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });

        // PUT /api/bids/{id}/award - Award a bid to a contractor
        app.MapPut("/api/bids/{id}/award", async (
            int id,
            int userId,
            BidPortalContext context,
            dynamic acceptanceInfo) =>
        {
            var bid = await context.Bids
                .Include(b => b.Project)
                .Include(b => b.Contractor)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bid == null)
            {
                return Results.NotFound();
            }

            // Authorization: Only the project owner can award bids
            if (bid.Project.OwnerId != userId)
            {
                return Results.Problem(
                    statusCode: StatusCodes.Status403Forbidden,
                    detail: "Only the project owner can award bids.");
            }

            // Business rule: Check if project is still open
            if (bid.Project.Status != "Open")
            {
                return Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    detail: "This project has already been awarded.");
            }

            // Require owner acceptance info (handle JsonElement)
            System.Text.Json.JsonElement elem = acceptanceInfo;
            if (!elem.TryGetProperty("ownerSignatures", out var ownerSignaturesElem) || ownerSignaturesElem.ValueKind != System.Text.Json.JsonValueKind.Array || ownerSignaturesElem.GetArrayLength() == 0)
            {
                return Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    detail: "Owner signatures are required to award a bid.");
            }

            bid.Status = "Accepted";
            bid.Project.Status = "Awarded";
            // Store owner acceptance info as array
            bid.OwnerSignaturesJson = ownerSignaturesElem.GetRawText();

            // Reject all other bids for this project
            var otherBids = await context.Bids
                .Where(b => b.ProjectId == bid.ProjectId && b.Id != id)
                .ToListAsync();

            foreach (var otherBid in otherBids)
            {
                if (otherBid.Status == "Pending")
                {
                    otherBid.Status = "Rejected";
                }
            }

            await context.SaveChangesAsync();

            return Results.Ok(new
            {
                message = "Bid awarded successfully",
                contractorName = $"{bid.Contractor.FirstName} {bid.Contractor.LastName}"
            });
        });
    }
}
