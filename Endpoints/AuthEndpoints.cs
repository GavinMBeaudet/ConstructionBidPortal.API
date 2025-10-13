using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConstructionBidPortal.API.Data;
using ConstructionBidPortal.API.Models;
using ConstructionBidPortal.API.DTOs;

namespace ConstructionBidPortal.API.Endpoints
{
    [ApiController]
    [Route("api/auth")]
    public class AuthEndpoints : ControllerBase
    {
        private readonly BidPortalContext _context;

        public AuthEndpoints(BidPortalContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // Check if user already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

            if (existingUser != null)
            {
                return Conflict("A user with this email already exists.");
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

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType
            };

            return CreatedAtAction(nameof(Register), new { id = user.Id }, userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // TODO: In production, verify hashed password
            if (user.PasswordHash != loginDto.Password)
            {
                return Unauthorized("Invalid email or password.");
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType
            };

            return Ok(userDto);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // In a real app with sessions/JWT, you'd invalidate the token here
            return NoContent();
        }
    }
}
