using BloodBank.Infrastructure.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BloodBank.Infrastructure.Services.Email;

public class EmailService(
    ISendGridClient sendGridClient,
    IOptions<SendGridConfig> sendGridConfig) : IEmailService
{
    private readonly ISendGridClient _sendGridClient = sendGridClient;
    private readonly SendGridConfig _sendGridConfig = sendGridConfig.Value;

    public async Task SendAsync(List<string> to, string subject, string body)
    {
        var msg = new SendGridMessage
        {
            From = new EmailAddress(_sendGridConfig.FromEmail, _sendGridConfig.FromName),
            Subject = subject,
        };

        msg.AddContent(MimeType.Text, body);

        foreach (var recipient in to)
            msg.AddTo(new EmailAddress(recipient));

        await _sendGridClient.SendEmailAsync(msg);
    }
}
