using BloodBank.Application.Results;
using MediatR;

namespace BloodBank.Application.Commands.DeleteBloodTransfer;

public class DeleteBloodTransferCommand(int id) : IRequest<Result>
{
    public int Id { get; private set; } = id;
}
