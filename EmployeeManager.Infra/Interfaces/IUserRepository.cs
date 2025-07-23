using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infra.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddUserAsync(User user);
}