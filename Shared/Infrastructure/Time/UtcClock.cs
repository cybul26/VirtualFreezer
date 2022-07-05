namespace VirtualFreezer.Shared.Infrastructure.Time;

internal class UtcClock : IClock
{
    public DateTime CurrentDate()
        => DateTime.UtcNow;
}