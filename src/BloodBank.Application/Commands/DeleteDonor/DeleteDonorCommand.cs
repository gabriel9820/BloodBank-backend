using BloodBank.Application.Results;
using MediatR;

namespace BloodBank.Application.Commands.DeleteDonor;

public class DeleteDonorCommand(int id) : IRequest<Result>
{
    public int Id { get; private set; } = id;
}
