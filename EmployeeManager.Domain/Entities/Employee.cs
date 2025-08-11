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
    public string PhoneNumber { get; private set; } = null!;
    public string Department { get; private set; } = null!;
    public string Position { get; private set; } = null!;
    public decimal Wage { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string CPF { get; set; } = null!;
    public DateTime HireDate { get; private set; }
    public DateTime? TerminationDate { get; private set; }
    public string EmploymentType { get; private set; } = null!;
    public bool IsActive { get; private set; }
    
    public string BankName { get; private set; } = null!;
    public string BankAccountNumber { get; private set; } = null!;
    
    public string EmergencyContactName { get; private set; } = null!;
    public string EmergencyContactPhone { get; private set; } = null!;
    
    public List<string> DocumentsIds { get; private set; } = new List<string>();

    public Employee() { }

    public Employee(string name, string email,string address)
    {
        Name = name;
        Email = email;
        Address = address;
    }

    public void Update(string name, string email, string address)
    {
        Name = name;
        Email = email;
        Address = address;
    }

    public void SetPhotoId(string photoId)
    {
        PhotoId = photoId;
    }
}