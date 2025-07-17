using BloodBank.Core.Constants;
using BloodBank.Core.Entities;

namespace BloodBank.UnitTests.Fakers;

public class DonationFaker : Faker<Donation>
{
    public DonationFaker() : base("pt_BR")
    {
        CustomInstantiator(f => new Donation(
            f.Date.Recent(1, DateTime.UtcNow),
            f.Random.Int(DonationRules.MIN_DONATION_QUANTITY_ML, DonationRules.MAX_DONATION_QUANTITY_ML),
            new DonorFaker().Generate()
        ));

        RuleFor(d => d.Id, f => f.Random.Int(1, 1000));
    }
}
