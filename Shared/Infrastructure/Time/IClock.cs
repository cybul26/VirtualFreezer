namespace VirtualFreezer.Shared.Infrastructure.Time;

internal interface IClock
{
    DateTime CurrentDate();
}