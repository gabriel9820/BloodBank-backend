using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using MediatR;

namespace BloodBank.Application.Queries.GetHospitalById;

public class GetHospitalByIdQuery(int id) : IRequest<Result<HospitalDetailsViewModel>>
{
    public int Id { get; private set; } = id;
}
