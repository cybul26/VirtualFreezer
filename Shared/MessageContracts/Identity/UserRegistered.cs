using System.Security.Cryptography;

namespace VirtualFreezer.MessageContracts.Identity;

public interface UserRegistered
{
    public string Email { get; set; }
}