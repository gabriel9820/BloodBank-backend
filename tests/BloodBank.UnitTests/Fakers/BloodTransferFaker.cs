using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.UnitTests.Fakers;

public class BloodTransferFaker : Faker<BloodTransfer>
{
    public BloodTransferFaker() : base("pt_BR")
    {
        CustomInstantiator(f => new BloodTransfer(
            f.Date.Recent(1, DateTime.UtcNow),
            f.PickRandom<BloodType>(),
            f.PickRandom<RhFactor>(),
            f.Random.Int(100, 1000),
            new HospitalFaker().Generate()
        ));

        RuleFor(bt => bt.Id, f => f.Random.Int(1, 1000));
    }
}
