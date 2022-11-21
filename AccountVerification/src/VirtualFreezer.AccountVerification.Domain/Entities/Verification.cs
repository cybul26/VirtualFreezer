using VirtualFreezer.AccountVerification.Domain.Exceptions;
using VirtualFreezer.AccountVerification.Domain.ValueObjects;

namespace VirtualFreezer.AccountVerification.Domain.Entities;

public class Verification
{
    public Email Email { get; set; }
    public string VerificationHash { get; private set; }
    public DateTime ValidUntil { get; private set; }
    public bool IsVerified { get; private set; }
    public int ResendsMade { get; private set; }

    private Verification(Email email, string verificationHash, DateTime validUntil, bool isVerified,
        int resendsMade)
    {
        Email = email;
        VerificationHash = verificationHash;
        ValidUntil = validUntil;
        IsVerified = isVerified;
        ResendsMade = resendsMade;
    }

    public static Verification Create(string email, string verificationHash, TimeSpan hashValidationTime, DateTime now)
    {
        var validUntil = CalculateNewValidateUntilDateTime(hashValidationTime, now);

        return new Verification(email, verificationHash, validUntil, false, 0);
    }

    private static DateTime CalculateNewValidateUntilDateTime(TimeSpan hashValidationTime, DateTime now) =>
        now.Add(hashValidationTime);

    public void Verify(DateTime now)
    {
        if (IsVerified)
        {
            throw new AccountAlreadyVerifiedException(Email);
        }

        if (ValidUntil < now)
        {
            throw new VerificationValidationDateExpired(Email);
        }

        IsVerified = true;
    }

    public void Resend(int maxResends, TimeSpan hashValidationTime, DateTime now)
    {
        if (IsVerified)
        {
            throw new AccountAlreadyVerifiedException(Email);
        }
        
        if (ResendsMade + 1 > maxResends)
        {
            throw new MaxResendsReachedException();
        }
        
        var validUntil = CalculateNewValidateUntilDateTime(hashValidationTime, now);

        ValidUntil = validUntil;
        ResendsMade++;
    }
}