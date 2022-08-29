namespace VirtualFreezer.Shared.Infrastructure.Time;

public class UtcClock : IClock
{
    public DateTime CurrentDate()
        => DateTime.UtcNow;
}