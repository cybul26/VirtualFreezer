using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using VirtualFreezer.Identity.Application.Exceptions;
using VirtualFreezer.Identity.Domain.Repositories;
using VirtualFreezer.Shared.Infrastructure.Auth;
using VirtualFreezer.Shared.Infrastructure.Security;

namespace VirtualFreezer.Identity.Application.Features.SignIn;

internal class SignInEndpoint : Endpoint<SignInRequest>
{
    private const string AccessTokenCookie = "__access-token";
    private readonly CookieOptions _cookieOptions;
    private readonly IAuthManager _authManager;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;

    public SignInEndpoint(CookieOptions cookieOptions, IAuthManager authManager,
        IUserRepository userRepository, IPasswordManager passwordManager)
    {
        _cookieOptions = cookieOptions;
        _authManager = authManager;
        _userRepository = userRepository;
        _passwordManager = passwordManager;
    }

    public override void Configure()
    {
        Post("sign-in");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignInRequest req, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(req.Email.ToLowerInvariant());

        if (user is null)
        {
            throw new InvalidCredentialsException(req.Email);
        }

        if (!_passwordManager.IsValid(req.Password, user!.PasswordHash))
        {
            throw new InvalidCredentialsException(req.Email);
        }

        if (!user.IsVerified)
        {
            throw new AccountNotVerifiedException();
        }

        var jwt = _authManager.CreateToken(user.Id);
        AddCookie(AccessTokenCookie, jwt.AccessToken);
    }

    private void AddCookie(string key, string value) => HttpContext.Response.Cookies.Append(key, value, _cookieOptions);
}