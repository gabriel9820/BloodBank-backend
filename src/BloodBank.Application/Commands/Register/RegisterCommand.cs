using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.ValueObjects;
using MediatR;

namespace BloodBank.Application.Commands.Register;

public class RegisterCommand : IRequest<Result<int>>
{
    public string FullName { get; set; } = string.Empty;
    public string CellPhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsLowStockNotificationEnabled { get; set; }
}

public static class RegisterCommandExtensions
{
    public static User ToEntity(this RegisterCommand command, string passwordHash)
    {
        return new User(
            fullName: command.FullName,
            cellPhoneNumber: new CellPhoneNumber(command.CellPhoneNumber),
            email: new Email(command.Email),
            passwordHash: passwordHash,
            role: command.Role,
            isLowStockNotificationEnabled: command.IsLowStockNotificationEnabled
        );
    }
}
