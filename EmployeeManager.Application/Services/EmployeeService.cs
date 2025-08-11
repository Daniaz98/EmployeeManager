using EmployeeManager.Application.DTO;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infra.Interfaces;
using EmployeeManager.Infra.Repositories;
using EmployeeManager.Infra.Services;
using Microsoft.AspNetCore.Http;


namespace EmployeeManager.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly GridFsService _gridFsService;

    public EmployeeService(IEmployeeRepository repository, GridFsService gridFsService)
    {
        _repository = repository;
        _gridFsService = gridFsService;
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
        
        if (dto.PhotoId != null && dto.PhotoId.Length > 0)
        {
            var photoId = await _gridFsService.UploadFileAsync(dto.PhotoId);
            emp.SetPhotoId(photoId);
        }
        
        await _repository.AddAsync(emp);
    }

    public async Task UpdateEmployeeAsync(string id, UpdateEmployeeDto dto)
    {
        var employee = await _repository.GetEmployeeById(id);
        
        if (employee == null) throw new Exception("Funcionário não encontrado");
        
        employee.Update(dto.Name, dto.Email,dto.Adress);
        
        if (dto.PhotoId != null && dto.PhotoId.Length > 0)
        {
            var photoId = await _gridFsService.UploadFileAsync(dto.PhotoId);
            employee.SetPhotoId(photoId);
        }

        await _repository.UpdateAsync(employee);
    }

    public async Task UploadEmployeePhotoAsync(string employeeId, IFormFile file)
    {
        var photoId = await _gridFsService.UploadFileAsync(file);
        
        var employee = await _repository.GetEmployeeById(employeeId);
        if (employee == null) throw new Exception("Funcionário não encontrado.");
        
        employee.SetPhotoId(photoId);
        
        await _repository.UpdateAsync(employee);
    }

    public async Task<Stream?> DownloadPhotoAsync(string employeeId)
    {
        var employee = await _repository.GetEmployeeById(employeeId);
        if (employee == null || string.IsNullOrEmpty(employee.PhotoId)) return null;
        
        return await _gridFsService.DownloadFileAsync(employee.PhotoId);
    }

    public async Task DeleteEmployeeAsync(string? id)
    {
        await _repository.DeleteAsync(id);
    }
}