using EmployeeManager.Domain.Entities;
using EmployeeManager.Infra.Interfaces;
using MongoDB.Driver;

namespace EmployeeManager.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _accounts;
    
    public async Task<User?> GetUserById(string id)
    {
        return await  _accounts.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _accounts.InsertOneAsync(user);
    }
}