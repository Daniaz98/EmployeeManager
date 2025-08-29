using System.ComponentModel.DataAnnotations;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Application.DTO;

public class LoginDto
{
    [Required(ErrorMessage = "Username é obrigatório")]
    [EmailAddress]
    public string Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password é obrigatório")]
    public string Password  { get; set; } = string.Empty;
}