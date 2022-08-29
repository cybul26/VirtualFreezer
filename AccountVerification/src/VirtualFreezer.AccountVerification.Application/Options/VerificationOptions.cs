namespace VirtualFreezer.AccountVerification.Application.Options;

public class VerificationOptions
{
    public TimeSpan HashValidationTime { get; set; } = TimeSpan.FromDays(1);
    public string VerificationUrl { get; set; } = default!;
}