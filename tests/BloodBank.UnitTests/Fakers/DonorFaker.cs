using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Fakers;

public class DonorFaker : Faker<Donor>
{
    public DonorFaker() : base("pt_BR")
    {
        CustomInstantiator(f => new Donor(
            f.Name.FullName(),
            new CellPhoneNumber(f.Phone.PhoneNumber("(##) 9####-####")),
            new Email(f.Internet.Email()),
            DateOnly.FromDateTime(f.Date.Past(20)),
            f.PickRandom<Gender>(),
            f.Random.Decimal(50, 100),
            f.PickRandom<BloodType>(),
            f.PickRandom<RhFactor>(),
            new AddressFaker().Generate()
        ));

        RuleFor(d => d.Id, f => f.Random.Int(1, 1000));
    }
}
