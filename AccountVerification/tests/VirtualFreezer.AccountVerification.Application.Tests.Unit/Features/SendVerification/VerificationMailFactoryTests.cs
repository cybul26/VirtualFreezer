using System;
using FluentAssertions;
using VirtualFreezer.AccountVerification.Application.Features.SendVerification;
using VirtualFreezer.AccountVerification.Application.Options;
using VirtualFreezer.Shared.Infrastructure.Mailing;
using Xunit;

namespace VirtualFreezer.AccountVerification.Application.Tests.Unit.Features.SendVerification;

public class VerificationMailFactoryTests
{
    [Fact]
    public void Create_should_create_message_with_valid_email()
    {
        var sendGridOptions = new SendGridOptions
        {
            ApiKey = "test",
            SenderEmail = "sender@test.pl",
            TemplateId = "test"
        };

        var verificationOptions = new VerificationOptions
        {
            VerificationUrl = "test.pl",
            HashValidationTime = TimeSpan.FromMilliseconds(1)
        };
        var sut = new VerificationMailFactory(sendGridOptions, verificationOptions);

        var actual = sut.Create("consumer@test.pl", "hash");

        actual.From.Email.Should().Be(sendGridOptions.SenderEmail);
        actual.TemplateId.Should().Be("test");
    }
}