using FastEndpoints;
using SendGrid;
using VirtualFreezer.AccountVerification.Application.Features.ResendVerification.Exceptions;
using VirtualFreezer.AccountVerification.Application.Features.SendVerification;
using VirtualFreezer.AccountVerification.Application.Features.Verify.Exceptions;
using VirtualFreezer.AccountVerification.Application.Options;
using VirtualFreezer.AccountVerification.Domain.Repositories;
using VirtualFreezer.Shared.Infrastructure.Time;

namespace VirtualFreezer.AccountVerification.Application.Features.ResendVerification;

public class ResendVerificationEndpoint : Endpoint<ResendVerificationRequest>
{
    private readonly IVerificationsRepository _repository;
    private readonly VerificationOptions _options;
    private readonly IClock _clock;
    private readonly IVerificationMailFactory _mailFactory;
    private readonly ISendGridClient _client;

    public override void Configure()
    {
        AllowAnonymous();
        Put("resend");
    }

    public ResendVerificationEndpoint(IVerificationsRepository repository, VerificationOptions options, IClock clock,
        IVerificationMailFactory mailFactory, ISendGridClient client)
    {
        _repository = repository;
        _options = options;
        _clock = clock;
        _mailFactory = mailFactory;
        _client = client;
    }

    public override async Task HandleAsync(ResendVerificationRequest req, CancellationToken ct)
    {
        var verification = await _repository.GetByEmailAsync(req.Email);

        if (verification is null)
        {
            throw new VerificationNotFoundException(req.Email);
        }

        verification.Resend(_options.MaxResends, _options.HashValidationTime, _clock.CurrentDate());

        await _repository.UpdateAsync(verification);
        var email = _mailFactory.Create(req.Email, verification.VerificationHash);
        await _client.SendEmailAsync(email, ct);
    }
}