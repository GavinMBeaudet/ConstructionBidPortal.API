using Microsoft.EntityFrameworkCore;
using ConstructionBidPortal.API.Data;
using ConstructionBidPortal.API.Models;
using ConstructionBidPortal.API.DTOs;

namespace ConstructionBidPortal.API.Endpoints;

public static class ProjectsEndpoints
{
    public static void MapProjectsEndpoints(this WebApplication app)
    {
        // GET /api/projects - Get all projects with optional category filtering
        app.MapGet("/api/projects", async (
            BidPortalContext context,
            string? categories) =>
        {
            var query = context.Projects
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

            var projects = await query.ToListAsync();
            return Results.Ok(projects);
        });

        // GET /api/projects/owner/{userId}/with-stats - Get owner's projects with bid statistics
        app.MapGet("/api/projects/owner/{userId}/with-stats", async (
            int userId,
            BidPortalContext context) =>
        {
            var projects = await context.Projects
                .Where(p => p.OwnerId == userId)
                .Include(p => p.Owner)
                .Include(p => p.ProjectCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Bids)
                    .ThenInclude(b => b.Contractor)
                .ToListAsync();

            var projectStats = projects.Select(p => new
            {
                p.Id,
                p.Title,
                p.Description,
                p.Location,
                p.Budget,
                p.BidDeadline,
                p.Status,
                p.DateCreated,
                p.OwnerId,
                Owner = new
                {
                    p.Owner.Id,
                    p.Owner.FirstName,
                    p.Owner.LastName,
                    p.Owner.Email
                },
                ProjectCategories = p.ProjectCategories.Select(pc => new
                {
                    pc.CategoryId,
                    pc.Category.Name,
                    pc.Category.Description
                }),
                BidCount = p.Bids.Count,
                AvgBid = p.Bids.Any() ? p.Bids.Average(b => b.BidAmount) : 0,
                LowestBid = p.Bids.Any() ? p.Bids.Min(b => b.BidAmount) : 0,
                HighestBid = p.Bids.Any() ? p.Bids.Max(b => b.BidAmount) : 0
            });

            return Results.Ok(projectStats);
        });

        // GET /api/projects/{id} - Get a specific project by ID
        app.MapGet("/api/projects/{id}", async (
            int id,
            BidPortalContext context) =>
        {
            var project = await context.Projects
                .Include(p => p.Owner)
                .Include(p => p.Bids)
                    .ThenInclude(b => b.Contractor)
                .Include(p => p.ProjectCategories)
                    .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(project);
        });

        // POST /api/projects - Create a new project
        app.MapPost("/api/projects", async (
            CreateProjectDto projectDto,
            BidPortalContext context) =>
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

            context.Projects.Add(project);
            await context.SaveChangesAsync();

            // Add project categories
            if (projectDto.CategoryIds != null && projectDto.CategoryIds.Any())
            {
                foreach (var categoryId in projectDto.CategoryIds)
                {
                    context.Add(new ProjectCategory
                    {
                        ProjectId = project.Id,
                        CategoryId = categoryId
                    });
                }
                await context.SaveChangesAsync();
            }

            return Results.Created($"/api/projects/{project.Id}", project);
        });

        // PUT /api/projects/{id} - Update an existing project
        app.MapPut("/api/projects/{id}", async (
            int id,
            UpdateProjectDto projectDto,
            BidPortalContext context) =>
        {
            if (id != projectDto.Id)
            {
                return Results.BadRequest("ID mismatch");
            }

            var existingProject = await context.Projects
                .Include(p => p.ProjectCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProject == null)
            {
                return Results.NotFound();
            }

            // Authorization: Only the owner can update their project
            if (existingProject.OwnerId != projectDto.OwnerId)
            {
                return Results.Problem(
                    statusCode: StatusCodes.Status403Forbidden,
                    detail: "You can only edit your own projects.");
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
                context.RemoveRange(existingProject.ProjectCategories);

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
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await context.Projects.AnyAsync(e => e.Id == id);
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

        // DELETE /api/projects/{id} - Delete a project
        app.MapDelete("/api/projects/{id}", async (
            int id,
            int userId,
            BidPortalContext context) =>
        {
            var project = await context.Projects.FindAsync(id);
            if (project == null)
            {
                return Results.NotFound();
            }

            // Authorization: Only the owner can delete their project
            if (project.OwnerId != userId)
            {
                return Results.Problem(
                    statusCode: StatusCodes.Status403Forbidden,
                    detail: "You can only delete your own projects.");
            }

            context.Projects.Remove(project);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}
