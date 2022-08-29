using FastEndpoints;
using MassTransit;
using Microsoft.Extensions.Logging;
using VirtualFreezer.AccountVerification.Application.Features.Verify.Exceptions;
using VirtualFreezer.AccountVerification.Domain.Repositories;
using VirtualFreezer.MessageContracts.AccountVerification;
using VirtualFreezer.Shared.Infrastructure.Time;

namespace VirtualFreezer.AccountVerification.Application.Features.Verify;

internal class VerifyAccountEndpoint : Endpoint<VerifyAccountRequest>
{
    private readonly IVerificationsRepository _repository;
    private readonly ILogger<VerifyAccountEndpoint> _logger;
    private readonly IClock _clock;
    private readonly IBus _bus;

    public VerifyAccountEndpoint(IVerificationsRepository repository, ILogger<VerifyAccountEndpoint> logger,
        IClock clock,
        IBus bus)
    {
        _repository = repository;
        _logger = logger;
        _clock = clock;
        _bus = bus;
    }

    public override void Configure()
    {
        Put("verify");
        AllowAnonymous();
    }

    public override async Task HandleAsync(VerifyAccountRequest req, CancellationToken ct)
    {
        var verification = await _repository.GetByHashAsync(req.VerificationHash);
        if (verification is null)
        {
            throw new VerificationHashNotFoundException(req.VerificationHash);
        }

        if (verification.IsVerified)
        {
            throw new AccountAlreadyVerifiedException(verification.UserId);
        }

        if (verification.ValidUntil < _clock.CurrentDate())
        {
            throw new VerificationValidationDateExpired(verification.Id);
        }

        verification.Verify();

        await _repository.UpdateAsync(verification);
        _logger.LogInformation("Verified user account. User id: '{UserId}'", verification.Id);
        await _bus.Publish(new AccountVerified(verification.UserId), ct);
    }
}