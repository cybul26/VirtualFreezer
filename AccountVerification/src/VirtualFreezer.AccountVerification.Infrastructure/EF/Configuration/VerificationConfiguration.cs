using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VirtualFreezer.AccountVerification.Domain.Entities;
using VirtualFreezer.AccountVerification.Domain.ValueObjects;

namespace VirtualFreezer.AccountVerification.Infrastructure.EF.Configuration;

internal class VerificationConfiguration : IEntityTypeConfiguration<Verification>
{
    public void Configure(EntityTypeBuilder<Verification> builder)
    {
        var emailConverter = new ValueConverter<Email, string>(
            v => v.Value,
            v => new Email(v));

        builder.HasKey(x => x.Email);
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.VerificationHash);
        builder.Property(x => x.Email).HasConversion(emailConverter).IsRequired();
        builder.Property<DateTime>("_validUntil").HasColumnName("ValidUntil");
        builder.Property<bool>("_isVerified").HasColumnName("IsVerified");

        builder.OwnsMany<Resend>("_resends", y =>
        {
            y.WithOwner().HasForeignKey(x => x.Email);
            y.HasKey(x => x.Id);
            y.Property(x => x.Email).HasConversion(emailConverter).IsRequired();
            y.ToTable("Resends");
            y.Property(x => x.Id).ValueGeneratedNever();
        });
        builder.Ignore(x => x.Resends);
    }
}