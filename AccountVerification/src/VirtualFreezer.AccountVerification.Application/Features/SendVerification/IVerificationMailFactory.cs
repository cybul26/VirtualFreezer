using SendGrid.Helpers.Mail;

namespace VirtualFreezer.AccountVerification.Application.Features.SendVerification;

public interface IVerificationMailFactory
{
    SendGridMessage Create(string customerEmail, string verificationHash);
}