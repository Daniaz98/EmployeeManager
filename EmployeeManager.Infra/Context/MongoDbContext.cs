using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using EmployeeManager.Domain.Entities;
using Microsoft.Extensions.Options;


namespace EmployeeManager.Infra.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    
    public MongoDbContext(IOptions<MongoDbSettings> settings) 
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<Employee> Employees =>
        _database.GetCollection<Employee>("employee");
    
    public IMongoDatabase Database => _database;
}