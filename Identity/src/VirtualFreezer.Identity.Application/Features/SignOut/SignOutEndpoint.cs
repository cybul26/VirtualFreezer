using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace VirtualFreezer.Identity.Application.Features.SignOut;

[HttpDelete("sign-out")]
internal class SignOutEndpoint : EndpointWithoutRequest
{
    private readonly ILogger<SignOutEndpoint> _logger;
    private const string AccessTokenCookie = "__access-token";


    public SignOutEndpoint(ILogger<SignOutEndpoint> logger)
    {
        _logger = logger;
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        _logger.LogInformation("Signing out user");
        HttpContext.Response.Cookies.Delete(AccessTokenCookie);
        return Task.CompletedTask;
    }
}