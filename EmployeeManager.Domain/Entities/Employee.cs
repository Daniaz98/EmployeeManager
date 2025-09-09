using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManager.Domain.Entities;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("Name")]
    public string Name { get; private set; } = null!;
    [BsonElement("Email")]
    public string Email { get; private set; }  = null!;
    [BsonRepresentation(BsonType.ObjectId)]
    public string? PhotoId { get; set; }
    [BsonElement("Address")]
    public string Address { get; set; }  = null!;
    [BsonElement("Department")]
    public string Department { get; set; } = null!;

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