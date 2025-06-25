using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using EmployeeManager.Domain.Entities;


namespace EmployeeManager.Infra.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    
    public MongoDbContext(IConfiguration configuration) 
    {
        var client = new MongoClient(configuration.GetConnectionString("Mongo"));
        _database = client.GetDatabase("Employee");
    }

    public IMongoCollection<Employee> Employees =>
        _database.GetCollection<Employee>("employee");
}