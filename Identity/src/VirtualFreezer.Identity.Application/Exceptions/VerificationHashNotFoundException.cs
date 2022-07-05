using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Identity.Application.Exceptions;

internal class VerificationHashNotFoundException : CustomException
{
    public VerificationHashNotFoundException(string hash) : base($"Verification hash: '{hash}' was not found")
    {
    }
}