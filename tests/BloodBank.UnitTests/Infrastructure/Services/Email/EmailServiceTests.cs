using BloodBank.Infrastructure.Models;
using BloodBank.Infrastructure.Services.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BloodBank.UnitTests.Infrastructure.Services.Email;

public class EmailServiceTests
{
    private readonly Mock<ISendGridClient> _sendGridClientMock;
    private readonly Mock<IOptions<SendGridConfig>> _sendGridConfigMock;
    private readonly EmailService _emailService;

    public EmailServiceTests()
    {
        _sendGridClientMock = new Mock<ISendGridClient>();
        _sendGridConfigMock = new Mock<IOptions<SendGridConfig>>();
        _sendGridConfigMock.Setup(config => config.Value).Returns(new SendGridConfig
        {
            ApiKey = "test_api_key",
            FromEmail = "test@email.com",
            FromName = "Test Sender"
        });
        _emailService = new EmailService(_sendGridClientMock.Object, _sendGridConfigMock.Object);
    }

    [Fact]
    public async Task SendAsync_ShouldSendEmailWithCorrectParameters()
    {
        // Arrange
        var fromEmail = _sendGridConfigMock.Object.Value.FromEmail;
        var fromName = _sendGridConfigMock.Object.Value.FromName;
        var recipients = new List<string>
        {
            "user1@email.com",
            "user2@email.com"
        };
        var subject = "Test Subject";
        var body = "Test Body";

        // Act
        await _emailService.SendAsync(recipients, subject, body);

        // Assert
        _sendGridClientMock.Verify(
            x => x.SendEmailAsync(
                It.Is<SendGridMessage>(msg =>
                    msg.From.Email == fromEmail &&
                    msg.From.Name == fromName &&
                    msg.Personalizations.SelectMany(p => p.Tos).Select(t => t.Email).ToList().Count == recipients.Count &&
                    msg.Subject == subject &&
                    msg.Contents.Any(c => c.Value == body)
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once
        );
    }
}
