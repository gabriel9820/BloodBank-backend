using BloodBank.Application.DTOs.InputModels;
using BloodBank.Application.Results;
using MediatR;

namespace BloodBank.Application.Commands.UpdateHospital;

public class UpdateHospitalCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LandlineNumber { get; set; } = string.Empty;
    public AddressInputModel Address { get; set; } = default!;
}
