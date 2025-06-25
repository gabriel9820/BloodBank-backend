using BloodBank.Application.DTOs.InputModels;
using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.ValueObjects;
using MediatR;

namespace BloodBank.Application.Commands.AddDonor;

public class AddDonorCommand : IRequest<Result<int>>
{
    public string FullName { get; set; } = string.Empty;
    public string CellPhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public decimal Weight { get; set; }
    public BloodType BloodType { get; set; }
    public RhFactor RhFactor { get; set; }
    public AddressInputModel Address { get; set; } = default!;
}

public static class AddDonorCommandExtensions
{
    public static Donor ToEntity(this AddDonorCommand command)
    {
        return new Donor(
            fullName: command.FullName,
            cellPhoneNumber: new CellPhoneNumber(command.CellPhoneNumber),
            email: new Email(command.Email),
            birthDate: command.BirthDate,
            gender: command.Gender,
            weight: command.Weight,
            bloodType: command.BloodType,
            rhFactor: command.RhFactor,
            address: command.Address.ToValueObject()
        );
    }
}
