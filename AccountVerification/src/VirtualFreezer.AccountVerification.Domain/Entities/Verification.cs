using VirtualFreezer.AccountVerification.Domain.Entities.BusinessRules;
using VirtualFreezer.AccountVerification.Domain.ValueObjects;
using VirtualFreezer.Shared.Abstractions;
using VirtualFreezer.Shared.Abstractions.Domain;

namespace VirtualFreezer.AccountVerification.Domain.Entities;

public class Verification : Entity
{
    private DateTime _validUntilUtc;
    private bool _isVerified;
    private readonly List<Resend> _resends = new();
    public Email Email { get; set; }
    public string VerificationHash { get; }

    public IReadOnlyCollection<Resend> Resends => _resends.AsReadOnly();

    private Verification(Email email, string verificationHash, DateTime validUntilUtc, bool isVerified)
    {
        Email = email;
        VerificationHash = verificationHash;
        _validUntilUtc = validUntilUtc;
        _isVerified = isVerified;
    }

    public static Verification Create(string email, string verificationHash, TimeSpan hashValidationTime)
    {
        var validUntil = CalculateNewValidateUntilDateTime(hashValidationTime);

        return new Verification(email, verificationHash, validUntil, false);
    }

    private static DateTime CalculateNewValidateUntilDateTime(TimeSpan hashValidationTime) =>
        SystemClock.Now.Add(hashValidationTime);

    public void Verify()
    {
        CheckRule(new CannotPerformIfAccountAlreadyVerifiedRule(_isVerified));
        CheckRule(new VerificationHashCannotBeExpiredRule(_validUntilUtc));
        _isVerified = true;
    }

    public void Resend(int maxResends, TimeSpan hashValidationTime, TimeSpan minimumTimeBetweenResends)
    {
        CheckRule(new CannotPerformIfAccountAlreadyVerifiedRule(_isVerified));
        CheckRule(new CannotExceedMaxResendsRule(maxResends, _resends));
        CheckRule(new CannotViolateMinimumTimeBetweenResendsRule(_resends, minimumTimeBetweenResends));

        var validUntil = CalculateNewValidateUntilDateTime(hashValidationTime);

        _validUntilUtc = validUntil;
        _resends.Add(new Resend(Guid.NewGuid(), SystemClock.Now, Email));
    }
}