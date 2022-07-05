using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Identity.Application.Exceptions;

internal class InvalidCredentialsExceptions : CustomException
{
    public InvalidCredentialsExceptions(string email) : base($"Invalid credentials for user with email: '{email}'")
    {
    }
}