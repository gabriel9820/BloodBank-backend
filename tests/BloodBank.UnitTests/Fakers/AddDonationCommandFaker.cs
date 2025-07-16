using BloodBank.Application.Commands.AddDonation;
using BloodBank.Core.Constants;

namespace BloodBank.UnitTests.Fakers;

public class AddDonationCommandFaker : Faker<AddDonationCommand>
{
    public AddDonationCommandFaker()
    {
        RuleFor(command => command.DonationDate, f => f.Date.Recent(1, DateTime.UtcNow));
        RuleFor(command => command.QuantityML, f => f.Random.Int(DonationRules.MIN_DONATION_QUANTITY_ML, DonationRules.MAX_DONATION_QUANTITY_ML));
        RuleFor(command => command.DonorId, f => f.Random.Int(1, 1000));
    }
}
