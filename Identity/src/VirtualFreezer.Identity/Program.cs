using VirtualFreezer.Identity.Application.Features.SignIn;
using VirtualFreezer.Identity.Application.Features.SignUp;
using VirtualFreezer.Identity.Infrastructure;
using VirtualFreezer.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddMicroFramework(o =>
{
    o.MapRequestToLog = (request) =>
        request switch
        {
            SignInRequest signIn => new
            {
                UserName = signIn.Email,
                Password = "*********"
            },
            SignUpRequest signUp => new
            {
                signUp.Email,
                Password = "*********"
            },
            _ => request
        };
});
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();
app.UseMicroFramework();
app.MapGet("/", () => "Identity Service");

app.Run();

//For testing purposes only
namespace VirtualFreezer.Identity
{
    public partial class Program
    {
    }
}