using System.ComponentModel.DataAnnotations;
using EmployeeManager.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManager.Application.DTO;

public class RegisterDto
{
    [Required(ErrorMessage = "Username é obrigatório")]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password é obrigatório")]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = "User";
}