using EmployeeManager.Domain.Entities;
using EmployeeManager.Infra.Context;
using EmployeeManager.Infra.Interfaces;
using MongoDB.Driver;

namespace EmployeeManager.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(MongoDbContext context)
    {
        _collection = context.Users;
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await  _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _collection.InsertOneAsync(user);
    }
}