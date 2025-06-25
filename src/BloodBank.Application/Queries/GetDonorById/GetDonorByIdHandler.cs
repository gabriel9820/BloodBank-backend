using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Queries.GetDonorById;

public class GetDonorByIdHandler(
    IDonorRepository donorRepository) : IRequestHandler<GetDonorByIdQuery, Result<DonorDetailsViewModel>>
{
    private readonly IDonorRepository _donorRepository = donorRepository;

    public async Task<Result<DonorDetailsViewModel>> Handle(GetDonorByIdQuery request, CancellationToken cancellationToken)
    {
        var donor = await _donorRepository.GetByIdAsync(request.Id);

        if (donor is null)
            return DonorErrors.DonorNotFound;

        return donor.ToDetailsViewModel();
    }
}
