using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using MediatR;

namespace BloodBank.Application.Commands.AddBloodTransfer;

public class AddBloodTransferCommand : IRequest<Result<int>>
{
    public DateTime TransferDate { get; set; }
    public BloodType BloodType { get; set; }
    public RhFactor RhFactor { get; set; }
    public int QuantityML { get; set; }
    public int HospitalId { get; set; }
}

public static class AddBloodTransferCommandExtensions
{
    public static BloodTransfer ToEntity(this AddBloodTransferCommand command, Hospital hospital)
    {
        return new BloodTransfer(
            transferDate: command.TransferDate,
            bloodType: command.BloodType,
            rhFactor: command.RhFactor,
            quantityML: command.QuantityML,
            hospital: hospital
        );
    }
}
