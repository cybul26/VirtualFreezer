using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using VirtualFreezer.Identity.Application.Exceptions;
using VirtualFreezer.Identity.Domain.Entities;
using VirtualFreezer.Identity.Domain.Repositories;
using VirtualFreezer.Shared.Infrastructure.Security;
using VirtualFreezer.Shared.Infrastructure.Security.Random;

namespace VirtualFreezer.Identity.Application.Features.SignUp;

[HttpPost("sign-up")]
[AllowAnonymous]
internal class SignUpEndpoint : Endpoint<SignUpRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IRng _rng;
    private readonly IPasswordManager _passwordManager;

    public SignUpEndpoint(IUserRepository userRepository, IRng rng, IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _rng = rng;
        _passwordManager = passwordManager;
    }

    public override async Task HandleAsync(SignUpRequest req, CancellationToken ct)
    {
        if (await _userRepository.GetByEmailAsync(req.Email) is { })
        {
            throw new AccountAlreadyExistsException(req.Email);
        }

        var verificationHash = _rng.Generate(32);
        var passwordHash = _passwordManager.Secure(req.Password);
        var user = new User(Guid.NewGuid(), req.Email, passwordHash, false, verificationHash);
        await _userRepository.AddAsync(user);
    }
}