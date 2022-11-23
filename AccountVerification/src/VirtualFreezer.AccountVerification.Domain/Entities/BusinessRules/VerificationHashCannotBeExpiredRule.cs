using VirtualFreezer.Shared.Abstractions;
using VirtualFreezer.Shared.Abstractions.Domain;

namespace VirtualFreezer.AccountVerification.Domain.Entities.BusinessRules;

public class VerificationHashCannotBeExpiredRule : IBusinessRule
{
    private readonly DateTime _hashValidUntil;

    public VerificationHashCannotBeExpiredRule(DateTime hashValidUntil)
    {
        _hashValidUntil = hashValidUntil;
    }

    public bool IsBroken() => _hashValidUntil < SystemClock.Now;


    public string Message => "Verification hash expired";
}