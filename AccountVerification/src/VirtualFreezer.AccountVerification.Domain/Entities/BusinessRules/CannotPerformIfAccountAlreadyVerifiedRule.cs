using VirtualFreezer.Shared.Abstractions.Domain;

namespace VirtualFreezer.AccountVerification.Domain.Entities.BusinessRules;

public class CannotPerformIfAccountAlreadyVerifiedRule : IBusinessRule
{
    private readonly bool _isVerified;

    public CannotPerformIfAccountAlreadyVerifiedRule(bool isVerified)
    {
        _isVerified = isVerified;
    }

    public bool IsBroken() => _isVerified;


    public string Message => "Account is already verified";
}