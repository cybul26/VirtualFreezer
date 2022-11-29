using VirtualFreezer.Shared.Abstractions.Domain;

namespace VirtualFreezer.AccountVerification.Domain.Entities.BusinessRules;

public class CannotPerformWhenAccountAlreadyVerifiedRule : IBusinessRule
{
    private readonly bool _isVerified;

    public CannotPerformWhenAccountAlreadyVerifiedRule(bool isVerified)
    {
        _isVerified = isVerified;
    }

    public bool IsBroken() => _isVerified;


    public string Message => "Account is already verified";
}