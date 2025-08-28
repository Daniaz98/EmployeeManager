using System.ComponentModel.DataAnnotations;
using EmployeeManager.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManager.Application.DTO;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [BsonRepresentation(BsonType.String)]
    [Required]
    public UserRole Role { get; set; } = UserRole.funcionario;
}