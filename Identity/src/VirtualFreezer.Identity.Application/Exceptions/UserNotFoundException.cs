using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Identity.Application.Exceptions;

public class UserNotFoundException : CustomException
{
    public UserNotFoundException(Guid userId) : base($"User with id: '{userId}' not found")
    {
    }
}