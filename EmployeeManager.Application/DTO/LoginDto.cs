using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Application.DTO;

public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password  { get; set; } = string.Empty;
}