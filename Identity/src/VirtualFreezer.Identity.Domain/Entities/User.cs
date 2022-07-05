using VirtualFreezer.Identity.Domain.ValueObjects;

namespace VirtualFreezer.Identity.Domain.Entities;

public class User
{
    public Guid Id { get;private set; }
    public Email Email { get;private set; } 
    public string PasswordHash { get;private set; } 
    public bool IsVerified { get; private set; }
    public string VerificationHash { get;private set; } 

    public User(Guid id, Email email, string passwordHash, bool isVerified, string verificationHash)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        IsVerified = isVerified;
        VerificationHash = verificationHash;
    }

    private User()
    {
    }

    public void Verify()
    {
        IsVerified = true;
    }
}