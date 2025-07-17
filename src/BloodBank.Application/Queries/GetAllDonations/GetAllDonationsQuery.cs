using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Models;
using MediatR;

namespace BloodBank.Application.Queries.GetAllDonations;

public class GetAllDonationsQuery : DonationPagedRequest, IRequest<Result<PagedResult<DonationListViewModel>>>
{

}
