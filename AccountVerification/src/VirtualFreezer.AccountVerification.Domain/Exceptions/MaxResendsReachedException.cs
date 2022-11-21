using VirtualFreezer.Shared.Abstractions.Exceptions;

namespace VirtualFreezer.AccountVerification.Domain.Exceptions;

public class MaxResendsReachedException : CustomException
{
    public MaxResendsReachedException() : base("Max resends reached")
    {
    }
}