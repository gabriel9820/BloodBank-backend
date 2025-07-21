namespace BloodBank.Infrastructure.Services.Email;

public interface IEmailService
{
    Task SendAsync(List<string> to, string subject, string body);
}
