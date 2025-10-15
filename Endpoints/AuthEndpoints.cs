using Microsoft.EntityFrameworkCore;
using ConstructionBidPortal.API.Data;
using ConstructionBidPortal.API.Models;
using ConstructionBidPortal.API.DTOs;

namespace ConstructionBidPortal.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        // POST /api/auth/register - Register a new user
        app.MapPost("/api/auth/register", async (
            RegisterDto registerDto,
            BidPortalContext context) =>
        {
            // Check if user already exists
            var existingUser = await context.Users
                .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

            if (existingUser != null)
            {
                return Results.Conflict("A user with this email already exists.");
            }

            // Create new user (in production, hash the password!)
            var user = new User
            {
                Email = registerDto.Email,
                PasswordHash = registerDto.Password, // TODO: Hash this with BCrypt or similar
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserType = registerDto.UserType,
                DateCreated = DateTime.Now
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType
            };

            return Results.Created($"/api/auth/register/{user.Id}", userDto);
        });

        // POST /api/auth/login - Login an existing user
        app.MapPost("/api/auth/login", async (
            LoginDto loginDto,
            BidPortalContext context) =>
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return Results.Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    detail: "Invalid email or password.");
            }

            // TODO: In production, verify hashed password
            if (user.PasswordHash != loginDto.Password)
            {
                return Results.Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    detail: "Invalid email or password.");
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType
            };

            return Results.Ok(userDto);
        });

        // POST /api/auth/logout - Logout user
        app.MapPost("/api/auth/logout", () =>
        {
            // In a real app with sessions/JWT, you'd invalidate the token here
            return Results.NoContent();
        });
    }
}
