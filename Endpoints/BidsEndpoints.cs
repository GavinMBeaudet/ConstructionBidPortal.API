using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConstructionBidPortal.API.Data;
using ConstructionBidPortal.API.Models;
using ConstructionBidPortal.API.DTOs;

namespace ConstructionBidPortal.API.Endpoints
{
    [ApiController]
    [Route("api/bids")]
    public class BidsEndpoints : ControllerBase
    {
        private readonly BidPortalContext _context;

        public BidsEndpoints(BidPortalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBids([FromQuery] int? projectId, [FromQuery] int? contractorId)
        {
            var query = _context.Bids
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

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bid>> GetBid(int id)
        {
            var bid = await _context.Bids
                .Include(b => b.Project)
                .Include(b => b.Contractor)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bid == null)
            {
                return NotFound();
            }

            return bid;
        }

        [HttpPost]
        public async Task<ActionResult<Bid>> CreateBid(CreateBidDto bidDto)
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

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBid), new { id = bid.Id }, bid);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(int id, UpdateBidDto bidDto)
        {
            if (id != bidDto.Id)
            {
                return BadRequest();
            }

            var existingBid = await _context.Bids.FindAsync(id);
            if (existingBid == null)
            {
                return NotFound();
            }

            // Authorization: Only the contractor who created the bid can update it
            if (existingBid.ContractorId != bidDto.ContractorId)
            {
                return Forbid("You can only edit your own bids.");
            }

            existingBid.BidAmount = bidDto.BidAmount;
            existingBid.TimelineInDays = bidDto.TimelineInDays;
            existingBid.Proposal = bidDto.Proposal;
            existingBid.Status = bidDto.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id, [FromQuery] int userId)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            // Authorization: Only the contractor who created the bid can delete it
            if (bid.ContractorId != userId)
            {
                return Forbid("You can only delete your own bids.");
            }

            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.Id == id);
        }
    }
}
