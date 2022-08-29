using FastEndpoints;
using FluentValidation;

namespace VirtualFreezer.Identity.Application.Features.SignIn;

internal class SignInRequestValidator : Validator<SignInRequest>
{
    public SignInRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().NotNull().EmailAddress();
        RuleFor(x => x.Password)
            .MinimumLength(5).WithMessage("your password is too short!");
       
    }
}