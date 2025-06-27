using BloodBank.Core.Entities;

namespace BloodBank.Application.DTOs.ViewModels;

public class HospitalDetailsViewModel(
    int id,
    string name,
    string landlineNumber,
    AddressViewModel address)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string LandlineNumber { get; private set; } = landlineNumber;
    public AddressViewModel Address { get; private set; } = address;
}

public static partial class HospitalExtensions
{
    public static HospitalDetailsViewModel ToDetailsViewModel(this Hospital hospital)
    {
        return new HospitalDetailsViewModel(
            hospital.Id,
            hospital.Name,
            hospital.LandlineNumber.Value,
            hospital.Address.ToViewModel()
        );
    }
}
