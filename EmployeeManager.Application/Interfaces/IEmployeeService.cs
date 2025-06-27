using EmployeeManager.Application.DTO;

namespace EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(string? id);
    Task AddEmployeeAsync(Employee employee);
    Task CreateEmployeeAsync(CreateEmployeeDto employee);
    Task UpdateEmployeeAsync(string id, UpdateEmployeeDto employee);
    Task DeleteEmployeeAsync(string? id);
}