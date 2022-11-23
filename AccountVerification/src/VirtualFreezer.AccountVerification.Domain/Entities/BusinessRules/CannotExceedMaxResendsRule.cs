using VirtualFreezer.Shared.Abstractions.Domain;

namespace VirtualFreezer.AccountVerification.Domain.Entities.BusinessRules;

public class CannotExceedMaxResendsRule : IBusinessRule
{
    private readonly int _maxResends;
    private readonly int _resendsMade;

    public CannotExceedMaxResendsRule(int maxResends, IReadOnlyCollection<Resend> resends)
    {
        _maxResends = maxResends;
        _resendsMade = resends.Count;
    }

    public bool IsBroken() => _resendsMade + 1 > _maxResends;
    public string Message => "Max resends reached";
}