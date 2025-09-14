using EmployeeManager.Application.DTO;

namespace EmployeeManager.Infra.Interfaces;

public interface IDepartmentRepository
{
    Task<List<DepartmentDto>> GetDepartment();
}