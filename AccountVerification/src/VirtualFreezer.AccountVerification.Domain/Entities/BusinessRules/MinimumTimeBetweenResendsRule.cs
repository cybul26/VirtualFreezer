using VirtualFreezer.Shared.Abstractions;
using VirtualFreezer.Shared.Abstractions.Domain;

namespace VirtualFreezer.AccountVerification.Domain.Entities.BusinessRules;

public class MinimumTimeBetweenResendsRule : IBusinessRule
{
    private readonly IReadOnlyCollection<Resend> _resends;
    private readonly TimeSpan _minimumTimeBetweenResends;

    public MinimumTimeBetweenResendsRule(IReadOnlyCollection<Resend> resends, TimeSpan minimumTimeBetweenResends)
    {
        _resends = resends;
        _minimumTimeBetweenResends = minimumTimeBetweenResends;
    }

    public bool IsBroken()
    {
        var lastSendResend = _resends.MaxBy(x => x.When);
        return lastSendResend is not null && lastSendResend.When.Add(_minimumTimeBetweenResends) > SystemClock.Now;
    }

    public string Message =>
        $"Cannot resend authentication hash because time between resends is in invalidation. Minimum minutes between resends: {_minimumTimeBetweenResends.TotalMinutes}";
}