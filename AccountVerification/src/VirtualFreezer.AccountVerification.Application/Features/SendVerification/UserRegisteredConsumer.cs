using MassTransit;
using Microsoft.Extensions.Logging;
using SendGrid;
using VirtualFreezer.AccountVerification.Application.Options;
using VirtualFreezer.AccountVerification.Domain.Entities;
using VirtualFreezer.AccountVerification.Domain.Repositories;
using VirtualFreezer.MessageContracts.Identity;
using VirtualFreezer.Shared.Infrastructure.Security.Random;

namespace VirtualFreezer.AccountVerification.Application.Features.SendVerification;

public class UserRegisteredConsumer : IConsumer<UserRegistered>
{
    private readonly ILogger<UserRegisteredConsumer> _logger;
    private readonly ISendGridClient _client;
    private readonly IVerificationsRepository _repository;
    private readonly IRng _rng;
    private readonly VerificationOptions _options;
    private readonly IVerificationMailFactory _mailFactory;

    public UserRegisteredConsumer(ILogger<UserRegisteredConsumer> logger, ISendGridClient client,
        IVerificationsRepository repository, IRng rng,
        VerificationOptions options, IVerificationMailFactory mailFactory)
    {
        _logger = logger;
        _client = client;
        _repository = repository;
        _rng = rng;
        _options = options;
        _mailFactory = mailFactory;
    }

    public async Task Consume(ConsumeContext<UserRegistered> context)
    {
        _logger.LogInformation("Sending verification email to address: {Email}", context.Message.Email);

        var verificationHash = _rng.Generate();
        var verification = Verification.Create(context.Message.Email, verificationHash,
            _options.HashValidationTime);
        await _repository.AddAsync(verification);
        var email = _mailFactory.Create(context.Message.Email, verificationHash);
        await _client.SendEmailAsync(email);
    }
}