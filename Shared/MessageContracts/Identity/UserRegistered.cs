using System.Security.Cryptography;

namespace VirtualFreezer.MessageContracts.Identity;

public interface UserRegistered
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
}