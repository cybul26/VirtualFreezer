using System;
using FluentAssertions;
using VirtualFreezer.AccountVerification.Domain.Entities;
using VirtualFreezer.Shared.Abstractions.Domain;
using Xunit;

namespace VirtualFreezer.AccountVerification.Domain.Tests.Unit.Entities;

public class VerificationTests
{
    [Fact]
    public void Verify_should_throw_if_account_already_verified()
    {
        var verification = Verification.Create("test@wp.pl", Guid.NewGuid().ToString("N"), TimeSpan.FromMinutes(10));
        verification.Verify();
        FluentActions.Invoking(() => verification.Verify()).Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Account is already verified");
    }

    [Fact]
    public void Verify_should_throw_if_verification_hash_expired()
    {
        var verification = Verification.Create("test@wp.pl", Guid.NewGuid().ToString("N"), TimeSpan.Zero);
        FluentActions.Invoking(() => verification.Verify()).Should().Throw<BusinessRuleValidationException>()
            .WithMessage("Verification hash expired");
    }

    [Fact]
    public void Resend_should_throw_if_minimum_time_between_resends_not_preserved()
    {
        var verification = Verification.Create("test@wp.pl", Guid.NewGuid().ToString("N"), TimeSpan.FromMinutes(10));
        verification.Resend(10, TimeSpan.FromHours(1), TimeSpan.FromMinutes(10));
        FluentActions.Invoking(() => verification.Resend(10, TimeSpan.FromHours(1), TimeSpan.FromMinutes(10))).Should()
            .Throw<BusinessRuleValidationException>().WithMessage(
                "Cannot resend authentication hash because time between resends is in invalidation. Minimum minutes between resends*");
    }
    
    [Fact]
    public void Resend_should_throw_if_maximum_attempts_reached()
    {
        var verification = Verification.Create("test@wp.pl", Guid.NewGuid().ToString("N"), TimeSpan.FromMinutes(10));
        verification.Resend(10, TimeSpan.FromHours(1), TimeSpan.FromMinutes(10));
        FluentActions.Invoking(() => verification.Resend(1, TimeSpan.FromHours(1), TimeSpan.Zero)).Should()
            .Throw<BusinessRuleValidationException>().WithMessage(
                "Max resends reached");
    }
}