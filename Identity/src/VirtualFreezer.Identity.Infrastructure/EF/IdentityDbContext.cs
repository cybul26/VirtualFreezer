using Microsoft.EntityFrameworkCore;
using VirtualFreezer.Identity.Domain.Entities;

namespace VirtualFreezer.Identity.Infrastructure.EF;

internal class IdentityDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}