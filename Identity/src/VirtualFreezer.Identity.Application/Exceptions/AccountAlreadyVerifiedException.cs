using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Identity.Application.Exceptions;

internal class AccountAlreadyVerifiedException : CustomException
{
    public AccountAlreadyVerifiedException(Guid userId) : base($"User with id: '{userId:N}' is already verified")
    {
    }
}