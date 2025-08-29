using EmployeeManager.Application.DTO;

namespace EmployeeManager.Application.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDto?> LoginAsync(LoginDto loginDto);
    Task<TokenResponseDto?> RegisterAsync(RegisterDto registerDto);
    Task<bool> ValidateUserAsync(string username);
}