using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using MediatR;

namespace BloodBank.Application.Commands.AddDonation;

public class AddDonationCommand : IRequest<Result<int>>
{
    public DateTime DonationDate { get; set; }
    public int QuantityML { get; set; }
    public int DonorId { get; set; }
}

public static class AddDonationCommandExtensions
{
    public static Donation ToEntity(this AddDonationCommand command, Donor donor)
    {
        return new Donation(
            command.DonationDate,
            command.QuantityML,
            donor
        );
    }
}
