using Microsoft.EntityFrameworkCore;
using ConstructionBidPortal.API.Data;
using ConstructionBidPortal.API.Models;

namespace ConstructionBidPortal.API.Endpoints;

public static class UsersEndpoints
{
    public static void MapUsersEndpoints(this WebApplication app)
    {
        // GET /api/users - Get all users
        app.MapGet("/api/users", async (BidPortalContext context) =>
        {
            var users = await context.Users.ToListAsync();
            return Results.Ok(users);
        });

        // GET /api/users/{id} - Get a specific user by ID
        app.MapGet("/api/users/{id}", async (
            int id,
            BidPortalContext context) =>
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(user);
        });

        // POST /api/users - Create a new user
        app.MapPost("/api/users", async (
            User user,
            BidPortalContext context) =>
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Results.Created($"/api/users/{user.Id}", user);
        });

        // PUT /api/users/{id} - Update an existing user
        app.MapPut("/api/users/{id}", async (
            int id,
            User user,
            BidPortalContext context) =>
        {
            if (id != user.Id)
            {
                return Results.BadRequest("ID mismatch");
            }

            context.Entry(user).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await context.Users.AnyAsync(e => e.Id == id);
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

        // DELETE /api/users/{id} - Delete a user
        app.MapDelete("/api/users/{id}", async (
            int id,
            BidPortalContext context) =>
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return Results.NotFound();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}
