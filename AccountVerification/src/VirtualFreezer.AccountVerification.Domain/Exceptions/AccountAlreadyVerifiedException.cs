using VirtualFreezer.Shared.Abstractions.Exceptions;

namespace VirtualFreezer.AccountVerification.Domain.Exceptions;

internal class AccountAlreadyVerifiedException : CustomException
{
    public AccountAlreadyVerifiedException(string email) : base($"User with email: '{email}' is already verified")
    {
    }
}