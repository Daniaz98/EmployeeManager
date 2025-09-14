using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Application.DTO;

public class SearchEmployeesDto
{
    public string SearchTerm { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Page deve ser maior que 0")]
    public int Page { get; set; } = 1;
    
    [Range(1, 100, ErrorMessage = "PageSize deve estar entre 1 e 100")]
    public int PageSize { get; set; } = 10;
}