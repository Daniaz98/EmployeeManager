using Microsoft.AspNetCore.Http;

namespace EmployeeManager.Application.DTO;

public class UpdateEmployeeDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Adress { get; set; }
    public IFormFile? PhotoId { get; set; }
}