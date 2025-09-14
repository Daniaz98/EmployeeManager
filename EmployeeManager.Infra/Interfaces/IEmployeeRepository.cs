using EmployeeManager.Domain.Entities;
using EmployeeManager.Application.DTO;
using EmployeeManager.Domain;
using EmployeeManager.Domain.ValueObjects;

namespace EmployeeManager.Infra.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployees();

    Task<Employee> GetEmployeeById(string? id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(string id);
    
    Task<SearchResult<Employee>> SearchAsync(
        ISpecification<Employee> specification, 
        SearchCriteria criteria, 
        CancellationToken cancellationToken = default);
}