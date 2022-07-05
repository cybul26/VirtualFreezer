using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using VirtualFreezer.Identity.Application.Exceptions;
using VirtualFreezer.Identity.Domain.Repositories;

namespace VirtualFreezer.Identity.Application.Features.Verify;

[HttpPut("verify")]
[AllowAnonymous]
internal class VerifyAccountEndpoint : Endpoint<VerifyAccountRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<VerifyAccountEndpoint> _logger;

    public VerifyAccountEndpoint(IUserRepository userRepository, ILogger<VerifyAccountEndpoint> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public override async Task HandleAsync(VerifyAccountRequest req, CancellationToken ct)
    {
        var user = await _userRepository.GetByVerificationHashAsync(req.VerificationHash);
        if (user is null)
        {
            throw new VerificationHashNotFoundException(req.VerificationHash);
        }

        if (user.IsVerified)
        {
            throw new AccountAlreadyVerifiedException(user.Id);
        }

        user.Verify();

        await _userRepository.UpdateAsync(user);
        _logger.LogInformation("Verified user account. User id: '{UserId}'", user.Id);
    }
}