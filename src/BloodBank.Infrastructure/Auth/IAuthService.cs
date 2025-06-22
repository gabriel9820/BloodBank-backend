namespace BloodBank.Infrastructure.Auth;

public interface IAuthService
{
    string HashPassword(string password);
    string GenerateJwtToken(string userId, string email, string role);
}
