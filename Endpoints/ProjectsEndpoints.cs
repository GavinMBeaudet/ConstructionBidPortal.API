using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConstructionBidPortal.API.Data;
using ConstructionBidPortal.API.Models;
using ConstructionBidPortal.API.DTOs;

namespace ConstructionBidPortal.API.Endpoints
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsEndpoints : ControllerBase
    {
        private readonly BidPortalContext _context;

        public ProjectsEndpoints(BidPortalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects([FromQuery] string? categories)
        {
            var query = _context.Projects
                .Include(p => p.Owner)
                .Include(p => p.ProjectCategories)
                    .ThenInclude(pc => pc.Category)
                .AsQueryable();

            // Filter by categories if provided
            if (!string.IsNullOrEmpty(categories))
            {
                var categoryIds = categories.Split(',')
                    .Select(c => int.TryParse(c.Trim(), out var id) ? id : 0)
                    .Where(id => id > 0)
                    .ToList();

                if (categoryIds.Any())
                {
                    query = query.Where(p => p.ProjectCategories
                        .Any(pc => categoryIds.Contains(pc.CategoryId)));
                }
            }

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Owner)
                .Include(p => p.Bids)
                    .ThenInclude(b => b.Contractor)
                .Include(p => p.ProjectCategories)
                    .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(CreateProjectDto projectDto)
        {
            var project = new Project
            {
                OwnerId = projectDto.OwnerId,
                Title = projectDto.Title,
                Description = projectDto.Description,
                Location = projectDto.Location,
                Budget = projectDto.Budget,
                BidDeadline = projectDto.BidDeadline,
                Status = projectDto.Status,
                DateCreated = DateTime.Now
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            // Add project categories
            if (projectDto.CategoryIds != null && projectDto.CategoryIds.Any())
            {
                foreach (var categoryId in projectDto.CategoryIds)
                {
                    _context.Add(new ProjectCategory
                    {
                        ProjectId = project.Id,
                        CategoryId = categoryId
                    });
                }
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, UpdateProjectDto projectDto)
        {
            if (id != projectDto.Id)
            {
                return BadRequest();
            }

            var existingProject = await _context.Projects
                .Include(p => p.ProjectCategories)
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (existingProject == null)
            {
                return NotFound();
            }

            // Authorization: Only the owner can update their project
            if (existingProject.OwnerId != projectDto.OwnerId)
            {
                return Forbid("You can only edit your own projects.");
            }

            existingProject.Title = projectDto.Title;
            existingProject.Description = projectDto.Description;
            existingProject.Location = projectDto.Location;
            existingProject.Budget = projectDto.Budget;
            existingProject.BidDeadline = projectDto.BidDeadline;
            existingProject.Status = projectDto.Status;

            // Update categories
            if (projectDto.CategoryIds != null)
            {
                // Remove existing categories
                _context.RemoveRange(existingProject.ProjectCategories);
                
                // Add new categories
                foreach (var categoryId in projectDto.CategoryIds)
                {
                    existingProject.ProjectCategories.Add(new ProjectCategory
                    {
                        ProjectId = id,
                        CategoryId = categoryId
                    });
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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
        public async Task<IActionResult> DeleteProject(int id, [FromQuery] int userId)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            // Authorization: Only the owner can delete their project
            if (project.OwnerId != userId)
            {
                return Forbid("You can only delete your own projects.");
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
