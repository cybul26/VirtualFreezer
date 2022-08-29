using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.AccountVerification.Application.Features.Verify.Exceptions;

internal class AccountAlreadyVerifiedException : CustomException
{
    public AccountAlreadyVerifiedException(Guid userId) : base($"User with id: '{userId:N}' is already verified")
    {
    }
}