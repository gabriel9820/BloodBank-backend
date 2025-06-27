using BloodBank.Application.Results;
using MediatR;

namespace BloodBank.Application.Commands.DeleteHospital;

public class DeleteHospitalCommand(int id) : IRequest<Result>
{
    public int Id { get; private set; } = id;
}
