using EmployeeManager.Domain.Entities;
using EmployeeManager.Infra.Context;
using EmployeeManager.Infra.Interfaces;
using MongoDB.Driver;

namespace EmployeeManager.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _accounts;

    public UserRepository(MongoDbContext context)
    {
        _accounts = context.Database.GetCollection<User>("Users");
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await  _accounts.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _accounts.InsertOneAsync(user);
    }
}