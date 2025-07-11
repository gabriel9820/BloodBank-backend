using BloodBank.Core.Entities;
using BloodBank.Core.Enums;

namespace BloodBank.UnitTests.Fakers;

public class StockFaker : Faker<Stock>
{
    public StockFaker() : base("pt_BR")
    {
        CustomInstantiator(f => new Stock(
            f.PickRandom<BloodType>(),
            f.PickRandom<RhFactor>(),
            f.Random.Int(1, 1000)
        ));

        RuleFor(s => s.Id, f => f.Random.Int(1, 1000));
    }
}
