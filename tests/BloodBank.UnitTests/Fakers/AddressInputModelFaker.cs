using BloodBank.Application.DTOs.InputModels;

namespace BloodBank.UnitTests.Fakers;

public class AddressInputModelFaker : Faker<AddressInputModel>
{
    public AddressInputModelFaker() : base("pt_BR")
    {
        RuleFor(a => a.Street, f => f.Address.StreetName());
        RuleFor(a => a.Number, f => f.Random.Number(1, 1000).ToString());
        RuleFor(a => a.Neighborhood, f => f.Address.SecondaryAddress());
        RuleFor(a => a.City, f => f.Address.City());
        RuleFor(a => a.State, f => f.Address.StateAbbr());
        RuleFor(a => a.ZipCode, f => f.Address.ZipCode("#####-###"));
    }
}
