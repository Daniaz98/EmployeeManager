using EmployeeManager.Application.DTO;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infra.DTO;
using EmployeeManager.Infra.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EmployeeManager.Infra.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IMongoCollection<Employee> _collection;

    public DepartmentRepository(IMongoCollection<Employee> collection)
    {
        _collection = collection;
    }

    public async Task<List<DepartmentDto>> GetDepartment()
    {
        return await _collection
            .AsQueryable()
            .Where(e => e.Department != null)
            .GroupBy(e => e.Department)
            .Select(g => new DepartmentDto
            {
                Department = g.Key,
                Employees = g.Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Address = e.Address,
                }).ToList()
            })
            .OrderBy(d => d.Department)
            .ToListAsync();
    }
}