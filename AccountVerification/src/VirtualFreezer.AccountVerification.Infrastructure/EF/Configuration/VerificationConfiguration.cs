using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualFreezer.AccountVerification.Domain.Entities;
using VirtualFreezer.AccountVerification.Domain.ValueObjects;

namespace VirtualFreezer.AccountVerification.Infrastructure.EF.Configuration;

internal class VerificationConfiguration : IEntityTypeConfiguration<Verification>
{
    public void Configure(EntityTypeBuilder<Verification> builder)
    {
        builder.HasKey(x => x.Email);
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.VerificationHash);
        builder.Property(x => x.Email).HasConversion(x => x.Value, x => new Email(x)).IsRequired();
    }
}