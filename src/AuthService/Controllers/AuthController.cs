using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // TODO: Replace with real authentication logic
        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        return Ok(new { token });
    }
}

public record LoginRequest(string Email, string Password);
