using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Fakers;

public class AddressFaker : Faker<Address>
{
    public AddressFaker() : base("pt_BR")
    {
        CustomInstantiator(f => new Address(
            f.Address.StreetName(),
            f.Random.Number(1, 1000).ToString(),
            f.Address.SecondaryAddress(),
            f.Address.City(),
            f.Address.StateAbbr(),
            f.Address.ZipCode("#####-###")
        ));
    }
}
