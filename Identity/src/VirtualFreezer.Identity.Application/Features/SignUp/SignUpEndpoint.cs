using FastEndpoints;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using SendGrid;
using SendGrid.Helpers.Mail;
using VirtualFreezer.Identity.Application.Exceptions;
using VirtualFreezer.Identity.Domain.Entities;
using VirtualFreezer.Identity.Domain.Repositories;
using VirtualFreezer.MessageContracts.Identity;
using VirtualFreezer.Shared.Infrastructure.Security;
using VirtualFreezer.Shared.Infrastructure.Security.Random;

namespace VirtualFreezer.Identity.Application.Features.SignUp;

internal class SignUpEndpoint : Endpoint<SignUpRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IPublishEndpoint _publishEndpoint;


    public SignUpEndpoint(IUserRepository userRepository,  IPasswordManager passwordManager,
         IPublishEndpoint publishEndpoint)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _publishEndpoint = publishEndpoint;
    }

    public override void Configure()
    {
        Post("sign-up");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignUpRequest req, CancellationToken ct)
    {
        if (await _userRepository.GetByEmailAsync(req.Email) is { })
        {
            throw new AccountAlreadyExistsException(req.Email);
        }

        var passwordHash = _passwordManager.Secure(req.Password);
        var user = new User(Guid.NewGuid(), req.Email, passwordHash, false);
        await _userRepository.AddAsync(user);
        await _publishEndpoint.Publish<UserRegistered>(new { UserId = user.Id, Email =  user.Email.Value }, ct);
    }
}