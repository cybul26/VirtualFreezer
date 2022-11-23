using Microsoft.EntityFrameworkCore;
using VirtualFreezer.AccountVerification.Domain.Entities;
using VirtualFreezer.AccountVerification.Domain.Repositories;
using VirtualFreezer.AccountVerification.Infrastructure.EF;

namespace VirtualFreezer.AccountVerification.Infrastructure.Repositories;

internal class VerificationsRepository : IVerificationsRepository
{
    private readonly VerificationDbContext _context;

    public VerificationsRepository(VerificationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Verification verification)
    {
        await _context.Verifications.AddAsync(verification);
        await _context.SaveChangesAsync();
    }

    public Task<Verification?> GetByHashAsync(string hash)
        => _context.Verifications.FirstOrDefaultAsync(x => x.VerificationHash == hash);

    public Task<Verification?> GetByEmailAsync(string email)
        => _context.Verifications.Include("_resends").FirstOrDefaultAsync(x => x.Email == email);


    public async Task UpdateAsync(Verification verification)
    {
        _context.Verifications.Update(verification);
        await _context.SaveChangesAsync();
    }
}