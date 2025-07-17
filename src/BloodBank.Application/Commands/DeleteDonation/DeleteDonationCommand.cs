using BloodBank.Application.Results;
using MediatR;

namespace BloodBank.Application.Commands.DeleteDonation;

public class DeleteDonationCommand(int id) : IRequest<Result>
{
    public int Id { get; private set; } = id;
}
