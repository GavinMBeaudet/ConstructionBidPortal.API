using Microsoft.EntityFrameworkCore;
using ConstructionBidPortal.API.Data;
using ConstructionBidPortal.API.Models;

namespace ConstructionBidPortal.API.Endpoints;

public static class CategoriesEndpoints
{
    public static void MapCategoriesEndpoints(this WebApplication app)
    {
        // GET /api/categories - Get all categories
        app.MapGet("/api/categories", async (BidPortalContext context) =>
        {
            var categories = await context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Results.Ok(categories);
        });
    }
}
