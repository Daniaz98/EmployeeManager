using EmployeeManager.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManager.Application.DTO;

public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    [BsonRepresentation(BsonType.String)]
    public UserRole Role { get; set; } = UserRole.funcionario;
    public string EmployeeId { get; set; }
}