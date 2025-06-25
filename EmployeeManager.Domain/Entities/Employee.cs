namespace EmployeeManager.Domain.Entities;

public class Employee
{
    public string Id { get; set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Photo { get; private set; }
    public string Address { get; private set; }

    public Employee() { }

    public Employee(string name, string email,string address)
    {
        Name = name;
        Email = email;
        Address = address;
    }
}