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
                BidAmount = b.BidAmount,
                TimelineInDays = b.TimelineInDays,
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
                }
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
            var bid = new Bid
            {
                ProjectId = bidDto.ProjectId,
                ContractorId = bidDto.ContractorId,
                BidAmount = bidDto.BidAmount,
                TimelineInDays = bidDto.TimelineInDays,
                Proposal = bidDto.Proposal,
                Status = "Submitted",
                DateSubmitted = DateTime.Now
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

            existingBid.BidAmount = bidDto.BidAmount;
            existingBid.TimelineInDays = bidDto.TimelineInDays;
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

            // Award the bid
            bid.Status = "Accepted";
            bid.Project.Status = "Awarded";

            // Reject all other bids for this project
            var otherBids = await context.Bids
                .Where(b => b.ProjectId == bid.ProjectId && b.Id != id)
                .ToListAsync();

            foreach (var otherBid in otherBids)
            {
                if (otherBid.Status == "Pending" || otherBid.Status == "Submitted")
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
