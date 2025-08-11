using AuthService.Data;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ApplicationDbContext context, PasswordHasher<User> passwordHasher) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (await context.Users.AnyAsync(u => u.Username == request.Username || u.Email == request.Email || u.PhoneNumber == request.PhoneNumber))
        {
            return Conflict("User with given details already exists.");
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };
        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(Login), new { identifier = request.Username }, new { user.Id, user.Username, user.Email, user.PhoneNumber });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await context.Users.FirstOrDefaultAsync(u =>
            u.Username == request.Identifier ||
            u.Email == request.Identifier ||
            u.PhoneNumber == request.Identifier);

        if (user is null)
        {
            return Unauthorized();
        }

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return Unauthorized();
        }

        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        return Ok(new { token });
    }
}

public record RegisterRequest(string Username, string Email, string PhoneNumber, string Password);
public record LoginRequest(string Identifier, string Password);
