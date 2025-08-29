using EmployeeManager.Domain.Entities;
using EmployeeManager.Infra.DTO;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManager.Application.DTO;

public class DepartmentDto
{
    public string Department { get; set; } = null!;
    
    [BsonElement("employees")]
    public List<EmployeeDto> Employees { get; set; } = new();
}