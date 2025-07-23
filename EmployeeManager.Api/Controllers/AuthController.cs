using EmployeeManager.Application.DTO;
using EmployeeManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var result =  await _authService.LoginAsync(loginDto);
        if (result == null)
            return Unauthorized("Credenciais inválidas!");
        
        return Ok(result);
    }
    
}