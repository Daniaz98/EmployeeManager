using EmployeeManager.Application.DTO;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infra.Interfaces;
using EmployeeManager.Infra.Repositories;

namespace EmployeeManager.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        return await _repository.GetAllEmployees();
    }

    public async Task<Employee> GetEmployeeByIdAsync(string? id)
    {
        return await _repository.GetEmployeeById(id);
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        await _repository.AddAsync(employee);
    }

    public async Task CreateEmployeeAsync(CreateEmployeeDto dto)
    {
        var emp = new Employee(dto.Name, dto.Email, dto.Address);
        
        await _repository.AddAsync(emp);
    }

    public async Task UpdateEmployeeAsync(string id, UpdateEmployeeDto dto)
    {
        var employee = await _repository.GetEmployeeById(id);
        
        if (id == null) throw new Exception("Funcionário não encontrado");
        
        employee.Update(dto.Name, dto.Email,dto.Adress);

        await _repository.UpdateAsync(employee);
    }

    public async Task DeleteEmployeeAsync(string? id)
    {
        await _repository.DeleteAsync(id);
    }
}