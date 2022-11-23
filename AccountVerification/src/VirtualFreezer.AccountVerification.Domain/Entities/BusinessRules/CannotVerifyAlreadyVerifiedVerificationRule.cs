using VirtualFreezer.Shared.Abstractions.Domain;

namespace VirtualFreezer.AccountVerification.Domain.Entities.BusinessRules;

public class CannotVerifyAlreadyVerifiedVerificationRule : IBusinessRule
{
    private readonly bool _isVerified;

    public CannotVerifyAlreadyVerifiedVerificationRule(bool isVerified)
    {
        _isVerified = isVerified;
    }

    public bool IsBroken() => _isVerified;


    public string Message => "Account is already verified";
}