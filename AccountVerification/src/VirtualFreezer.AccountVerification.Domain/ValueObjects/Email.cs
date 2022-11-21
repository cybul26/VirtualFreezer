using System.Text.RegularExpressions;

namespace VirtualFreezer.AccountVerification.Domain.ValueObjects;

public record Email
{
    public string Value { get; }

    public Email(string email)
    {
        Value = email.ToLowerInvariant();
    }
    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string value) => new(value);

    public override string ToString() => Value;
}