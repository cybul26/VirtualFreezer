using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Identity.Domain.Exceptions;

internal class InvalidEmailAddressFormatException : CustomException
{
    public InvalidEmailAddressFormatException(string email) : base($"Email: '{email}' is in invalid format")
    {
    }
}