using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using MediatR;

namespace BloodBank.Application.Queries.GetDonorById;

public class GetDonorByIdQuery(
    int id) : IRequest<Result<DonorDetailsViewModel>>
{
    public int Id { get; private set; } = id;
}
