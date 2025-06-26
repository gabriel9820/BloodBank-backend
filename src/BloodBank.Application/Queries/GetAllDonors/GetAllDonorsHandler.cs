using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Queries.GetAllDonors;

public class GetAllDonorsHandler(
    IDonorRepository donorRepository) : IRequestHandler<GetAllDonorsQuery, Result<PagedResult<DonorListViewModel>>>
{
    private readonly IDonorRepository _donorRepository = donorRepository;

    public async Task<Result<PagedResult<DonorListViewModel>>> Handle(GetAllDonorsQuery request, CancellationToken cancellationToken)
    {
        var pagedDonors = await _donorRepository.GetAllAsync(request);

        return new PagedResult<DonorListViewModel>(
            pagedDonors.Data.Select(d => d.ToListViewModel()).ToList(),
            pagedDonors.PageNumber,
            pagedDonors.PageSize,
            pagedDonors.TotalRecords,
            pagedDonors.TotalPages
        );
    }
}
