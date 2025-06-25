using BloodBank.Core.ValueObjects;

namespace BloodBank.Application.DTOs.ViewModels;

public class AddressViewModel(
    string street,
    string number,
    string neighborhood,
    string city,
    string state,
    string zipCode)
{
    public string Street { get; private set; } = street;
    public string Number { get; private set; } = number;
    public string Neighborhood { get; private set; } = neighborhood;
    public string City { get; private set; } = city;
    public string State { get; private set; } = state;
    public string ZipCode { get; private set; } = zipCode;
}

public static class AddressExtensions
{
    public static AddressViewModel ToViewModel(this Address address)
    {
        return new AddressViewModel(
            street: address.Street,
            number: address.Number,
            neighborhood: address.Neighborhood,
            city: address.City,
            state: address.State,
            zipCode: address.ZipCode
        );
    }
}
