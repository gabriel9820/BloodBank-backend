using BloodBank.Core.Entities;
using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Fakers;

public class HospitalFaker : Faker<Hospital>
{
    public HospitalFaker() : base("pt_BR")
    {
        CustomInstantiator(f => new Hospital(
            f.Company.CompanyName(),
            new LandlineNumber(f.Phone.PhoneNumber("(##) ####-####")),
            new AddressFaker().Generate()
        ));

        RuleFor(h => h.Id, f => f.Random.Int(1, 1000));
    }
}
