using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManager.Domain.Entities;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [Required]
    public string Name { get; private set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; private set; }  = null!;
    [BsonRepresentation(BsonType.ObjectId)]
    public string? PhotoId { get; private set; }
    public string Address { get; private set; }  = null!;
    public string Department { get; private set; } = null!;
    public List<ObjectId> DocumentIds { get; private set; } = new List<ObjectId>();

    public Employee() { }

    public Employee(string name, string email,string address, string department)
    {
        Name = name;
        Email = email;
        Address = address;
        Department = department;
    }

    public void Update(string name, string email, string address, string department)
    {
        Name = name;
        Email = email;
        Address = address;
        Department = department;
    }

    public void SetPhotoId(string photoId)
    {
        PhotoId = photoId;
    }
}