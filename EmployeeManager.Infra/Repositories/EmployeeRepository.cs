using EmployeeManager.Domain.Entities;
using EmployeeManager.Infra.Context;
using EmployeeManager.Infra.Interfaces;
using MongoDB.Driver;

namespace EmployeeManager.Infra.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IMongoCollection<Employee> _collection;

    public EmployeeRepository(MongoDbContext context)
    {
        _collection = context.Employees;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        return await _collection.Find(_ => true).ToListAsync(); 
    }

    public async Task<Employee> GetEmployeeById(string? id)
    {
        return await _collection.Find(d => d.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Employee employee)
    {
         await _collection.InsertOneAsync(employee);
    }

    public async Task UpdateAsync(Employee employee)
    {
        await _collection.ReplaceOneAsync(d => d.Id == employee.Id, employee);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(d => d.Id == id);
    }
}