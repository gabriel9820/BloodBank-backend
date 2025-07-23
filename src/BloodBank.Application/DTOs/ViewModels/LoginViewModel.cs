namespace BloodBank.Application.DTOs.ViewModels;

public class LoginViewModel(AuthUserViewModel user, string accessToken, string refreshToken)
{
    public AuthUserViewModel User { get; private set; } = user;
    public string AccessToken { get; private set; } = accessToken;
    public string RefreshToken { get; private set; } = refreshToken;
}

public class AuthUserViewModel(
    string fullName,
    string email,
    string role)
{
    public string FullName { get; private set; } = fullName;
    public string Email { get; private set; } = email;
    public string Role { get; private set; } = role;
}
