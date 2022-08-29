using AccountConfirmation.Api;
using AccountConfirmation.Api.EF;
using MassTransit;
using SendGrid.Extensions.DependencyInjection;
using VirtualFreezer.AccountVerification;
using VirtualFreezer.AccountVerification.Application;
using VirtualFreezer.AccountVerification.Infrastructure;
using VirtualFreezer.Shared.Infrastructure;
using VirtualFreezer.Shared.Infrastructure.DAL;

var builder = WebApplication.CreateBuilder(args);
builder.AddMicroFramework();
var configuration = builder.Configuration;
builder.Services
    .AddInfrastructure(configuration)
    .AddApplication(configuration);

var app = builder.Build();
app.UseMicroFramework();
app.MapGet("/", () => "Account verification");

app.Run();