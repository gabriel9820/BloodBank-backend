using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.Application.DTOs.ViewModels;

public class DonorListViewModel(
    int id,
    string fullName,
    string cellPhoneNumber,
    BloodType bloodType,
    RhFactor rhFactor)
{
    public int Id { get; private set; } = id;
    public string FullName { get; private set; } = fullName;
    public string CellPhoneNumber { get; private set; } = cellPhoneNumber;
    public BloodType BloodType { get; private set; } = bloodType;
    public RhFactor RhFactor { get; private set; } = rhFactor;
}

public static partial class DonorExtensions
{
    public static DonorListViewModel ToListViewModel(this Donor donor)
    {
        return new DonorListViewModel(
            id: donor.Id,
            fullName: donor.FullName,
            cellPhoneNumber: donor.CellPhoneNumber.Value,
            bloodType: donor.BloodType,
            rhFactor: donor.RhFactor
        );
    }
}
