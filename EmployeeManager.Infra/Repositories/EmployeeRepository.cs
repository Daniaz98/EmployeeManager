using EmployeeManager.Application.DTO;
using EmployeeManager.Domain;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.ValueObjects;
using EmployeeManager.Infra.Context;
using EmployeeManager.Infra.Interfaces;
using MongoDB.Bson;
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
    
    public async Task<SearchResult<Employee>> SearchAsync(
        ISpecification<Employee> specification, 
        SearchCriteria criteria, 
        CancellationToken cancellationToken = default)
    {
        if (specification == null)
            throw new ArgumentNullException(nameof(specification));

        if (criteria == null)
            throw new ArgumentNullException(nameof(criteria));

        var expression = specification.ToExpression();
        var filter = Builders<Employee>.Filter.Where(expression);

        var totalCount = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
        
        var skip = (criteria.Page - 1) * criteria.PageSize;
            
        var employees = await _collection
            .Find(filter)
            .Skip(skip)
            .Limit(criteria.PageSize)
            .ToListAsync(cancellationToken);

        return new SearchResult<Employee>(employees, (int)totalCount, criteria.Page, criteria.PageSize);
    }
    
}