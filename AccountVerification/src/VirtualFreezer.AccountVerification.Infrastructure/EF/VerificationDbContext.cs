using Microsoft.EntityFrameworkCore;
using VirtualFreezer.AccountVerification.Domain.Entities;

namespace VirtualFreezer.AccountVerification.Infrastructure.EF;

internal class VerificationDbContext : DbContext
{
    public DbSet<Verification> Verifications { get; set; }

    public VerificationDbContext(DbContextOptions<VerificationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}