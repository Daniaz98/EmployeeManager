using EmployeeManager.Infra.DTO;

namespace EmployeeManager.Application.DTO;

public class EmployeeSearchResultDto
{
    public IReadOnlyList<EmployeeDto> Items { get; set; }
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
