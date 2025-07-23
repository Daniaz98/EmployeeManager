using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infra.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserById(string id);
    Task AddUserAsync(User user);
}