using BloodBank.Application.DTOs.InputModels;
using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.ValueObjects;
using MediatR;

namespace BloodBank.Application.Commands.AddHospital;

public class AddHospitalCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = string.Empty;
    public string LandlineNumber { get; set; } = string.Empty;
    public AddressInputModel Address { get; set; } = default!;
}

public static class AddHospitalCommandExtensions
{
    public static Hospital ToEntity(this AddHospitalCommand command)
    {
        return new Hospital(
            name: command.Name,
            landlineNumber: new LandlineNumber(command.LandlineNumber),
            address: command.Address.ToValueObject()
        );
    }
}
