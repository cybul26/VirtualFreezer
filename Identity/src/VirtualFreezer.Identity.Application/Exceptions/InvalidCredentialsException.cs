using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Identity.Application.Exceptions;

internal class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException(string email) : base($"Invalid credentials for user with email: '{email}'")
    {
    }
}