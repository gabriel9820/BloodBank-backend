using BloodBank.Application.Commands.AddBloodTransfer;
using BloodBank.Core.Enums;

namespace BloodBank.UnitTests.Fakers;

public class AddBloodTransferCommandFaker : Faker<AddBloodTransferCommand>
{
    public AddBloodTransferCommandFaker()
    {
        RuleFor(x => x.TransferDate, f => f.Date.Recent(1, DateTime.UtcNow));
        RuleFor(x => x.BloodType, f => f.PickRandom<BloodType>());
        RuleFor(x => x.RhFactor, f => f.PickRandom<RhFactor>());
        RuleFor(x => x.QuantityML, f => f.Random.Int(100, 1000));
        RuleFor(x => x.HospitalId, f => f.Random.Int(1, 100));
    }
}
