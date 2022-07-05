using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;

namespace VirtualFreezer.Identity.Application.Exceptions;

internal class AccountNotVerifiedException : CustomException
{
    public AccountNotVerifiedException() : base("Account is not verified")
    {
    }
}