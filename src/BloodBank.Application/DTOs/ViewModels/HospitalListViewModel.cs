using BloodBank.Core.Entities;

namespace BloodBank.Application.DTOs.ViewModels;

public class HospitalListViewModel(
    int id,
    string name,
    string landlineNumber)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string LandlineNumber { get; private set; } = landlineNumber;
}

public static partial class HospitalExtensions
{
    public static HospitalListViewModel ToListViewModel(this Hospital hospital)
    {
        return new HospitalListViewModel(
            id: hospital.Id,
            name: hospital.Name,
            landlineNumber: hospital.LandlineNumber.Value
        );
    }
}
