using FastEndpoints;
using FluentValidation;

namespace VirtualFreezer.Identity.Application.Features.SignUp;

internal class SignUpValidator : Validator<SignUpRequest>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}