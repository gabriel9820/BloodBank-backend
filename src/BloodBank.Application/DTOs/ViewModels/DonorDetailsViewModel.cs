using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.Application.DTOs.ViewModels;

public class DonorDetailsViewModel(
    int id,
    string fullName,
    string cellPhoneNumber,
    string email,
    DateOnly birthDate,
    Gender gender,
    decimal weight,
    BloodType bloodType,
    RhFactor rhFactor,
    AddressViewModel address)
{
    public int Id { get; private set; } = id;
    public string FullName { get; private set; } = fullName;
    public string CellPhoneNumber { get; private set; } = cellPhoneNumber;
    public string Email { get; private set; } = email;
    public DateOnly BirthDate { get; private set; } = birthDate;
    public Gender Gender { get; private set; } = gender;
    public decimal Weight { get; private set; } = weight;
    public BloodType BloodType { get; private set; } = bloodType;
    public RhFactor RhFactor { get; private set; } = rhFactor;
    public AddressViewModel Address { get; private set; } = address;
}

public static class DonorExtensions
{
    public static DonorDetailsViewModel ToDetailsViewModel(this Donor donor)
    {
        return new DonorDetailsViewModel(
            id: donor.Id,
            fullName: donor.FullName,
            cellPhoneNumber: donor.CellPhoneNumber.Value,
            email: donor.Email.Value,
            birthDate: donor.BirthDate,
            gender: donor.Gender,
            weight: donor.Weight,
            bloodType: donor.BloodType,
            rhFactor: donor.RhFactor,
            address: donor.Address.ToViewModel()
        );
    }
}
