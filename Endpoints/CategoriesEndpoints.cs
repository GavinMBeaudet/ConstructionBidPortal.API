using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConstructionBidPortal.API.Data;
using ConstructionBidPortal.API.Models;

namespace ConstructionBidPortal.API.Endpoints
{
    public static class CategoriesEndpoints
    {
        public static void MapCategoriesEndpoints(this WebApplication app)
        {
            // GET /categories - Get all categories
            app.MapGet("/categories", async (BidPortalContext db) =>
            {
                var categories = await db.Categories
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                return Results.Ok(categories);
            })
            .WithName("GetCategories")
            .WithTags("Categories");
        }
    }
}
