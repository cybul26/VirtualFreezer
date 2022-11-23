namespace VirtualFreezer.AccountVerification.Application.Options;

public class VerificationOptions
{
    public TimeSpan HashValidationTime { get; set; } = TimeSpan.FromDays(1);
    public TimeSpan MinimumTimeBetweenResends { get; set; } = TimeSpan.FromMinutes(10);
    public string VerificationUrl { get; set; } = default!;
    public int MaxResends { get; set; } = 10;
}