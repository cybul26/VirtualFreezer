using VirtualFreezer.Shared.Abstractions.Exceptions;

namespace VirtualFreezer.AccountVerification.Domain.Exceptions;

public class VerificationValidationDateExpired : CustomException
{
    public VerificationValidationDateExpired(string email) : base(
        $"Verification for email: '{email}' cannot be verified because verification date expired")
    {
    }
}