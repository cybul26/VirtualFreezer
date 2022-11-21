using VirtualFreezer.Shared.Abstractions.Exceptions;
using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Identity.Application.Exceptions;

internal class AccountAlreadyExistsException : CustomException
{
    public AccountAlreadyExistsException(string email) : base($"Account with email: {email} already exists")
    {
    }
}