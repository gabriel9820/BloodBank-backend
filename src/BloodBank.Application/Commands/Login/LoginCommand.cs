using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using MediatR;

namespace BloodBank.Application.Commands.Login;

public class LoginCommand : IRequest<Result<LoginViewModel>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
