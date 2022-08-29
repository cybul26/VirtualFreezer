using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.AccountVerification.Application.Features.Verify.Exceptions;

public class VerificationValidationDateExpired : CustomException
{
    public VerificationValidationDateExpired(Guid verificationId) : base(
        $"Verification with id: '{verificationId}' cannot be verified because verification date expired")
    {
    }
}