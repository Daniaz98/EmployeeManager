namespace EmployeeManager.Application.DTO;

public class CreateEmployeeDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; }  = null!;
    public string? FotoId { get; set; }
}