namespace BloodBank.Infrastructure.Auth;

public interface IAuthService
{
    string GenerateJwtToken(string userId, string email, string role);
    string GenerateRefreshToken();
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
