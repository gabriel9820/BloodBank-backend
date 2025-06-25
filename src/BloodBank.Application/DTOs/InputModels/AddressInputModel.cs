using BloodBank.Core.ValueObjects;

namespace BloodBank.Application.DTOs.InputModels;

public class AddressInputModel
{
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}

public static class AddressInputModelExtensions
{
    public static Address ToValueObject(this AddressInputModel model)
    {
        return new Address(
            street: model.Street,
            number: model.Number,
            neighborhood: model.Neighborhood,
            city: model.City,
            state: model.State,
            zipCode: model.ZipCode
        );
    }
}
