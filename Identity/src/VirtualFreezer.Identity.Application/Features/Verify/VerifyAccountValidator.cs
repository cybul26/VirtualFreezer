using FastEndpoints;
using FluentValidation;

namespace VirtualFreezer.Identity.Application.Features.Verify;

internal class VerifyAccountValidator : Validator<VerifyAccountRequest>
{
    public VerifyAccountValidator()
    {
        RuleFor(x => x.VerificationHash).NotEmpty().NotNull();
    }
}