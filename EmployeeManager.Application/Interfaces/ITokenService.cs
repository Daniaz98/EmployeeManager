using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
    string? ValidateToken(string token);
    DateTime GetTokenExpiration();
}