using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using VirtualFreezer.Identity.Application.Features.SignIn;
using VirtualFreezer.Identity.Domain.Entities;
using VirtualFreezer.Identity.Domain.ValueObjects;
using VirtualFreezer.Identity.Infrastructure.EF;
using VirtualFreezer.Shared.Infrastructure.Exceptions.Models;
using VirtualFreezer.Shared.Infrastructure.Security;
using VirtualFreezer.Shared.Testing;
using Xunit;

namespace VirtualFreezer.Identity.Tests.Integration.Features.SignIn;

public class SignInEndpointTests : WebApiTestBase<Program>
{
    public SignInEndpointTests(TestApplicationFactory<Program> factory) : base(factory, typeof(IdentityDbContext))
    {
        SetPath("/");
    }

    [Fact]
    public async Task sign_in_should_return_bad_request_when_user_does_not_exists()
    {
        var request = new SignInRequest
        {
            Email = "test@123.pl",
            Password = "test123"
        };

        var response = await PostAsync("sign-in",
            request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errorResponse = await ReadAsync<Error>(response);
        errorResponse!.Code.Should().Be("invalid_credentials");
    }

    [Fact]
    public async Task sign_in_should_return_no_content_when_user_sing_in_is_succeeded()
    {
        var request = new SignInRequest
        {
            Email = "test@123.pl",
            Password = "test123"
        };

        var passwordHasher = GetRequiredService<IPasswordManager>();
        var passwordHash = passwordHasher.Secure(request.Password);
        var user = new User(Guid.NewGuid(), new Email(request.Email), passwordHash, true);
        await AddAsync(user);

        var response = await PostAsync("sign-in",
            request);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task sign_in_should_return_bad_request_when_user_password_doesnt_match()
    {
        var request = new SignInRequest
        {
            Email = "test@123.pl",
            Password = "test123"
        };

        var passwordHasher = GetRequiredService<IPasswordManager>();
        var passwordHash = passwordHasher.Secure("test1234");
        var user = new User(Guid.NewGuid(), new Email(request.Email), passwordHash, true);
        await AddAsync(user);

        var response = await PostAsync("sign-in",
            request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errorResponse = await ReadAsync<Error>(response);
        errorResponse!.Code.Should().Be("invalid_credentials");
    }
}