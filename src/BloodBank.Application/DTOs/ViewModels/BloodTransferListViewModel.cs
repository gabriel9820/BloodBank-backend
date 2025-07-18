using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.Application.DTOs.ViewModels;

public class BloodTransferListViewModel(
    int id,
    DateTime transferDate,
    BloodType bloodType,
    RhFactor rhFactor,
    int quantityML,
    HospitalListViewModel hospital)
{
    public int Id { get; private set; } = id;
    public DateTime TransferDate { get; private set; } = transferDate;
    public BloodType BloodType { get; private set; } = bloodType;
    public RhFactor RhFactor { get; private set; } = rhFactor;
    public int QuantityML { get; private set; } = quantityML;
    public HospitalListViewModel Hospital { get; private set; } = hospital;
}

public static partial class BloodTransferExtensions
{
    public static BloodTransferListViewModel ToListViewModel(this BloodTransfer bloodTransfer)
    {
        return new BloodTransferListViewModel(
            bloodTransfer.Id,
            bloodTransfer.TransferDate,
            bloodTransfer.BloodType,
            bloodTransfer.RhFactor,
            bloodTransfer.QuantityML,
            bloodTransfer.Hospital.ToListViewModel()
        );
    }
}
