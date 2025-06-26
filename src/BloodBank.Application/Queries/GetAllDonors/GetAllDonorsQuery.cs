using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Models;
using MediatR;

namespace BloodBank.Application.Queries.GetAllDonors;

public class GetAllDonorsQuery : DonorPagedRequest, IRequest<Result<PagedResult<DonorListViewModel>>>
{

}
