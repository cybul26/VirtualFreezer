using VirtualFreezer.Shared.Abstractions.Exceptions;
using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.AccountVerification.Application.Features.Verify.Exceptions;

internal class VerificationHashNotFoundException : CustomException
{
    public VerificationHashNotFoundException(string hash) : base($"Verification hash: '{hash}' was not found")
    {
    }
}