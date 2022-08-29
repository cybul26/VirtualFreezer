using SendGrid.Helpers.Mail;
using VirtualFreezer.AccountVerification.Application.Options;
using VirtualFreezer.Shared.Infrastructure.Mailing;

namespace VirtualFreezer.AccountVerification.Application.Features.SendVerification;

public class VerificationMailFactory : IVerificationMailFactory
{
    private readonly SendGridOptions _sendGridOptions;
    private readonly VerificationOptions _verificationOptions;

    public VerificationMailFactory(SendGridOptions sendGridOptions, VerificationOptions verificationOptions)
    {
        _sendGridOptions = sendGridOptions;
        _verificationOptions = verificationOptions;
    }

    public SendGridMessage Create(string customerEmail, string verificationHash)
    {
        var email = MailHelper.CreateSingleTemplateEmail(new EmailAddress(_sendGridOptions.SenderEmail),
            new EmailAddress(customerEmail), _sendGridOptions.TemplateId, new
            {
                url = _verificationOptions.VerificationUrl + verificationHash
            });

        return email;
    }
}