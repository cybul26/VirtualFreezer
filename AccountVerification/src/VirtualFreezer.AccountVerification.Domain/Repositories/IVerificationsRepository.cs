using VirtualFreezer.AccountVerification.Domain.Entities;

namespace VirtualFreezer.AccountVerification.Domain.Repositories;

public interface IVerificationsRepository
{
    Task AddAsync(Verification verification);
    Task<Verification?> GetByHashAsync(string hash);
    Task<Verification?> GetByEmailAsync(string email);
    Task UpdateAsync(Verification verification);
}