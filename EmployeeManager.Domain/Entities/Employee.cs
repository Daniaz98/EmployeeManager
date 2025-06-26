using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManager.Domain.Entities;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; }  = null!;
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string? PhotoId { get; private set; }
    public string Address { get; private set; }  = null!;

    public Employee() { }

    public Employee(string name, string email,string address)
    {
        Name = name;
        Email = email;
        Address = address;
    }
}