using EmployeeManager.Domain.Entities;
using EmployeeManager.Application.DTO;

namespace EmployeeManager.Infra.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployees();

    Task<Employee> GetEmployeeById(string? id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(string id);
}