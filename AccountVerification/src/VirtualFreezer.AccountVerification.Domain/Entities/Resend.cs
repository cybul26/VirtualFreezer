using VirtualFreezer.AccountVerification.Domain.ValueObjects;

namespace VirtualFreezer.AccountVerification.Domain.Entities;

public class Resend
{
    public Guid Id { get; private set; }
    public DateTime WhenUtc { get; private set; }
    public Email Email { get; private set; }

    public Resend(Guid id, DateTime whenUtc, Email email)
    {
        Id = id;
        WhenUtc = whenUtc;
        Email = email;
    }
}