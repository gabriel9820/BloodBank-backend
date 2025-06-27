using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Models;
using MediatR;

namespace BloodBank.Application.Queries.GetAllHospitals;

public class GetAllHospitalsQuery : HospitalPagedRequest, IRequest<Result<PagedResult<HospitalListViewModel>>>
{

}
