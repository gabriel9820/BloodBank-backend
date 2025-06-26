using BloodBank.Application.DTOs.InputModels;
using BloodBank.Application.Results;
using BloodBank.Core.Enums;
using MediatR;

namespace BloodBank.Application.Commands.UpdateDonor;

public class UpdateDonorCommand : IRequest<Result>
{
    public int Id { get; set; }
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
