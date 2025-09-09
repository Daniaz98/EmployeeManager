using EmployeeManager.Application.DTO;
using Microsoft.AspNetCore.Http;

namespace EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(string id);
    Task AddEmployeeAsync(Employee employee);
    Task CreateEmployeeAsync(CreateEmployeeDto employee);
    Task UpdateEmployeeAsync(string id, UpdateEmployeeDto employee);
    Task UploadEmployeePhotoAsync(string employeeId, IFormFile file);
    Task<Stream> DownloadPhotoAsync(string employeeId);
    Task DeleteEmployeeAsync(string? id);
    Task<List<DepartmentDto>> GetDepartmentsAsync();
    Task<EmployeeSearchResultDto> SearchEmployeesAsync(SearchEmployeesDto searchDto, CancellationToken cancellationToken = default);

}