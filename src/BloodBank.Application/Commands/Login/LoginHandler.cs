using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Auth;
using MediatR;

namespace BloodBank.Application.Commands.Login;

public class LoginHandler(
    IAuthService authService,
    IUserRepository userRepository) : IRequestHandler<LoginCommand, Result<LoginViewModel>>
{
    private readonly IAuthService _authService = authService;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<LoginViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null || !user.IsActive || !_authService.VerifyPassword(request.Password, user.PasswordHash))
            return UserErrors.InvalidCredentials;

        var authUser = new AuthUserViewModel(user.FullName, user.Email.Value, user.Role);
        var accessToken = _authService.GenerateJwtToken(user.Id.ToString(), user.Email.Value, user.Role);
        var refreshToken = _authService.GenerateRefreshToken();

        return new LoginViewModel(authUser, accessToken, refreshToken);
    }
}
