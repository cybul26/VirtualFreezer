using VirtualFreezer.Shared.Abstractions.Exceptions;

namespace VirtualFreezer.AccountVerification.Application.Features.ResendVerification.Exceptions;

public class VerificationNotFoundException : CustomException
{
    public VerificationNotFoundException(string email) : base($"Verification with email: '{email}' was not found")
    {
    }
}