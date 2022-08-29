namespace VirtualFreezer.AccountVerification.Domain.Entities;

public class Verification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string VerificationHash { get; set; }
    public DateTime ValidUntil { get; set; }
    public bool IsVerified { get; set; }

    public Verification(Guid id, Guid userId, string verificationHash, DateTime validUntil)
    {
        Id = id;
        UserId = userId;
        VerificationHash = verificationHash;
        ValidUntil = validUntil;
        IsVerified = false;
    }

    private Verification()
    {
    }

    public void Verify()
        => IsVerified = true;
}