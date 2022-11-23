using VirtualFreezer.AccountVerification.Domain.ValueObjects;

namespace VirtualFreezer.AccountVerification.Domain.Entities;

public class Resend
{
    public Guid Id { get; private set; }
    public DateTime When { get; private set; }
    public Email Email { get; private set; }

    public Resend(Guid id, DateTime when, Email email)
    {
        Id = id;
        When = when;
        Email = email;
    }
}