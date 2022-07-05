using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualFreezer.Identity.Domain.Entities;
using VirtualFreezer.Identity.Domain.ValueObjects;

namespace VirtualFreezer.Identity.Infrastructure.EF.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email).HasConversion(x => x.Value, x => new Email(x)).IsRequired();
        builder.HasIndex(x => x.VerificationHash).IsUnique();
    }
}