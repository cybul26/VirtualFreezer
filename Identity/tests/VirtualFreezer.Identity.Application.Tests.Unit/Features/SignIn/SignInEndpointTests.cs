using System;
using System.Threading.Tasks;
using AutoBogus;
using Bogus;
using FastEndpoints;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using VirtualFreezer.Identity.Application.Exceptions;
using VirtualFreezer.Identity.Application.Features.SignIn;
using VirtualFreezer.Identity.Domain.Entities;
using VirtualFreezer.Identity.Domain.Repositories;
using VirtualFreezer.Identity.Domain.ValueObjects;
using VirtualFreezer.Shared.Infrastructure.Auth;
using VirtualFreezer.Shared.Infrastructure.Security;
using Xunit;

namespace VirtualFreezer.Identity.Application.Tests.Unit.Features.SignIn;

public class SignInEndpointTests
{
    private readonly SignInEndpoint _sut;
    private readonly IAuthManager _authManager = Substitute.For<IAuthManager>();
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IPasswordManager _passwordManager = Substitute.For<IPasswordManager>();

    public SignInEndpointTests()
    {
        _sut = Factory.Create<SignInEndpoint>(new Faker<CookieOptions>().Generate(), _authManager, _userRepository,
            _passwordManager);
    }

    [Fact]
    public async Task handle_should_throw_invalid_credentials_exception_when_user_does_not_exists()
    {
        var request = new SignInRequest
        {
            Email = "test@123.pl",
            Password = "test"
        };

        _userRepository.GetByEmailAsync(request.Email).ReturnsNull();
        var exception = await Record.ExceptionAsync(() => _sut.HandleAsync(request, default));
        exception.Should().BeOfType<InvalidCredentialsException>();
    }

    [Fact]
    public async Task handle_should_throw_invalid_credentials_exception_when_user_password_doesnt_match()
    {
        var request = new SignInRequest
        {
            Email = "test@123.pl",
            Password = "test"
        };
        var user = new User(Guid.NewGuid(), new Email(request.Email), "hash", true);

        _userRepository.GetByEmailAsync(request.Email).Returns(user);
        _passwordManager.IsValid(request.Password, user.PasswordHash).Returns(false);

        var exception = await Record.ExceptionAsync(() => _sut.HandleAsync(request, default));
        exception.Should().BeOfType<InvalidCredentialsException>();
    }

    [Fact]
    public async Task handle_should_throw_user_not_verified_exception_when_user_is_not_verified()
    {
        var request = new SignInRequest
        {
            Email = "test@123.pl",
            Password = "test"
        };
        var user = new User(Guid.NewGuid(), new Email(request.Email), "hash", false);

        _userRepository.GetByEmailAsync(request.Email).Returns(user);
        _passwordManager.IsValid(request.Password, user.PasswordHash).Returns(true);

        var exception = await Record.ExceptionAsync(() => _sut.HandleAsync(request, default));
        exception.Should().BeOfType<AccountNotVerifiedException>();
    }

    [Fact]
    public async Task handle_should_create_token_when_all_data_are_correct()
    {
        var request = new SignInRequest
        {
            Email = "test@123.pl",
            Password = "test"
        };
        var user = new User(Guid.NewGuid(), new Email(request.Email), "hash", true);

        _userRepository.GetByEmailAsync(request.Email).Returns(user);
        _passwordManager.IsValid(request.Password, user.PasswordHash).Returns(true);
        _authManager.CreateToken(user.Id).Returns(new AutoFaker<JsonWebToken>().Generate());
        await _sut.HandleAsync(request, default);
        _authManager.Received(1).CreateToken(user.Id);
    }
}