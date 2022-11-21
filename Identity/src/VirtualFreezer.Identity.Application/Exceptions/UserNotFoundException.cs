using VirtualFreezer.Shared.Abstractions.Exceptions;
using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Identity.Application.Exceptions;

public class UserNotFoundException : CustomException
{
    public UserNotFoundException(Guid userId) : base($"User with id: '{userId}' was not found")
    {
    }
    
    public UserNotFoundException(string email) : base($"User with email: '{email}' was not found")
    {
    }
}