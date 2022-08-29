using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualFreezer.AccountVerification.Domain.Entities;

namespace VirtualFreezer.AccountVerification.Infrastructure.EF.Configuration;

internal class VerificationConfiguration : IEntityTypeConfiguration<Verification>
{
    public void Configure(EntityTypeBuilder<Verification> builder)
    {
        builder.HasKey(x => x.Id);
    }
}