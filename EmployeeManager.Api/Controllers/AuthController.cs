using EmployeeManager.Application.DTO;
using EmployeeManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Api.Controllers;

[ApiController]
[Route("authentication")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result =  await _authService.LoginAsync(loginDto);
        
        if (result == null)
            return Unauthorized(new { message = "Credenciais inválidas" });
        
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await _authService.RegisterAsync(registerDto);
        
        if (result == null)
            return BadRequest("Usuário já existe!");
        
        return Ok(result);
    }
    
}