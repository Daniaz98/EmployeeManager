using EmployeeManager.Application.DTO;

namespace EmployeeManager.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResultDto> LoginAsync(LoginDto loginDto);
}