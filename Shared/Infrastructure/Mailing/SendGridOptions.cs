namespace VirtualFreezer.Shared.Infrastructure.Mailing;

public class SendGridOptions
{
    public string ApiKey { get; set; }
    public string? TemplateId { get; set; }
    public string SenderEmail { get; set; }
}