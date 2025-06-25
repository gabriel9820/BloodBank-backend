namespace BloodBank.Application.DTOs.ViewModels;

public class LoginViewModel(string accessToken, string refreshToken)
{
    public string AccessToken { get; private set; } = accessToken;
    public string RefreshToken { get; private set; } = refreshToken;
}
