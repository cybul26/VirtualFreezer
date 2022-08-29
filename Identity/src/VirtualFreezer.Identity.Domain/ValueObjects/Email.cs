using System.Text.RegularExpressions;
using VirtualFreezer.Identity.Domain.Exceptions;

namespace VirtualFreezer.Identity.Domain.ValueObjects;

public record Email
{
    private const string EmailRegex =
        @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    public string Value { get; }

    public Email(string email)
    {
        if (!Regex.IsMatch(email, EmailRegex))
        {
            throw new InvalidEmailAddressFormatException(email);
        }

        Value = email.ToLowerInvariant();
    }
    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string value) => new(value);

    public override string ToString() => Value;
}