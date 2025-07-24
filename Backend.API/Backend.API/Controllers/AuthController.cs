using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginService _loginService;

    public AuthController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var token = await _loginService.LoginAsync(request);

        if (token == null)
            return Unauthorized("Credenciales inválidas");

        return Ok(new { Token = token });
    }
}
