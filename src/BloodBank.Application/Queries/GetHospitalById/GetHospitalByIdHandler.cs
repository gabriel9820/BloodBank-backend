using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Queries.GetHospitalById;

public class GetHospitalByIdHandler(
    IHospitalRepository hospitalRepository) : IRequestHandler<GetHospitalByIdQuery, Result<HospitalDetailsViewModel>>
{
    private readonly IHospitalRepository _hospitalRepository = hospitalRepository;

    public async Task<Result<HospitalDetailsViewModel>> Handle(GetHospitalByIdQuery request, CancellationToken cancellationToken)
    {
        var hospital = await _hospitalRepository.GetByIdAsync(request.Id);

        if (hospital is null)
            return HospitalErrors.HospitalNotFound;

        return hospital.ToDetailsViewModel();
    }
}
