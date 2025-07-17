using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Queries.GetAllDonations;

public class GetAllDonationsHandler(
    IDonationRepository donationRepository) : IRequestHandler<GetAllDonationsQuery, Result<PagedResult<DonationListViewModel>>>
{
    private readonly IDonationRepository _donationRepository = donationRepository;

    public async Task<Result<PagedResult<DonationListViewModel>>> Handle(GetAllDonationsQuery request, CancellationToken cancellationToken)
    {
        var pagedDonations = await _donationRepository.GetAllAsync(request);

        return new PagedResult<DonationListViewModel>(
            pagedDonations.Data.Select(d => d.ToListViewModel()).ToList(),
            pagedDonations.PageNumber,
            pagedDonations.PageSize,
            pagedDonations.TotalRecords,
            pagedDonations.TotalPages
        );
    }
}
