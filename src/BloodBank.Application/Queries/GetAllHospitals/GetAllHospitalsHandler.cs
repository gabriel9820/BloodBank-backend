using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Queries.GetAllHospitals;

public class GetAllHospitalsHandler(
    IHospitalRepository hospitalRepository) : IRequestHandler<GetAllHospitalsQuery, Result<PagedResult<HospitalListViewModel>>>
{
    private readonly IHospitalRepository _hospitalRepository = hospitalRepository;

    public async Task<Result<PagedResult<HospitalListViewModel>>> Handle(GetAllHospitalsQuery request, CancellationToken cancellationToken)
    {
        var pagedHospitals = await _hospitalRepository.GetAllAsync(request);

        return new PagedResult<HospitalListViewModel>(
            pagedHospitals.Data.Select(h => h.ToListViewModel()),
            pagedHospitals.PageNumber,
            pagedHospitals.PageSize,
            pagedHospitals.TotalRecords,
            pagedHospitals.TotalPages
        );
    }
}
