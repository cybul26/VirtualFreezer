using VirtualFreezer.AccountVerification.Domain.Entities;

namespace VirtualFreezer.AccountVerification.Domain.Repositories;

public interface IVerificationsRepository
{
    Task AddAsync(Verification verification);
    Task<Verification?> GetByHashAsync(string hash);
    Task UpdateAsync(Verification verification);
}