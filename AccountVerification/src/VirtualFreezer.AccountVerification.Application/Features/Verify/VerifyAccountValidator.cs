using FastEndpoints;
using FluentValidation;

namespace VirtualFreezer.AccountVerification.Application.Features.Verify;

internal class VerifyAccountValidator : Validator<VerifyAccountRequest>
{
    public VerifyAccountValidator()
    {
        RuleFor(x => x.VerificationHash).NotEmpty().NotNull();
    }
}