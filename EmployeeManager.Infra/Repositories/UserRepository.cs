using EmployeeManager.Domain.Entities;
using EmployeeManager.Infra.Context;
using EmployeeManager.Infra.Interfaces;
using MongoDB.Driver;

namespace EmployeeManager.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(MongoDbContext context)
    {
        _users = context.Users;
    }
    
    public async Task<User?> GetByIdAsync(string id)
    {
        return await _users.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _users.Find(x => x.Username == username).FirstOrDefaultAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _users.Find(x => x.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User> CreateAsync(User user)
    {
        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        await _users.ReplaceOneAsync(x => x.Id == user.Id, user);
    }

    public async Task DeleteAsync(string id)
    {
        await _users.DeleteOneAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _users.Find(_ => true).ToListAsync();
    }
}