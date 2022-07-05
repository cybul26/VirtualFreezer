using Microsoft.EntityFrameworkCore;
using VirtualFreezer.Identity.Domain.Entities;
using VirtualFreezer.Identity.Domain.Repositories;
using VirtualFreezer.Identity.Domain.ValueObjects;
using VirtualFreezer.Identity.Infrastructure.EF;

namespace VirtualFreezer.Identity.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _dbContext;

    public UserRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        var userEmail = new Email(email);
        return _dbContext.Users.SingleOrDefaultAsync(x => x.Email == userEmail);
    }

    public Task<User?> GetByVerificationHashAsync(string hash)
        => _dbContext.Users.SingleOrDefaultAsync(x => x.VerificationHash == hash);

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}