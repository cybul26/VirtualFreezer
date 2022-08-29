using VirtualFreezer.Identity.Domain.Entities;

namespace VirtualFreezer.Identity.Domain.Repositories;

public interface IUserRepository
{
    public Task<User?> GetByEmailAsync(string email);
    public Task<User?> GetByIdAsync(Guid id);
    public Task UpdateAsync(User user);
    public Task AddAsync(User user);
}