using System.Text.Json.Serialization;

namespace VirtualFreezer.Identity.Application.Features.SignIn;

public class SignInRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}