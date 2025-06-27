using BloodBank.Application.DTOs.InputModels;
using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using BloodBank.Core.ValueObjects;
using MediatR;

namespace BloodBank.Application.Commands.UpdateHospital;

public class UpdateHospitalHandler(
    IHospitalRepository hospitalRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateHospitalCommand, Result>
{
    private readonly IHospitalRepository _hospitalRepository = hospitalRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateHospitalCommand request, CancellationToken cancellationToken)
    {
        var hospital = await _hospitalRepository.GetByIdAsync(request.Id);

        if (hospital is null)
            return HospitalErrors.HospitalNotFound;

        hospital.Update(
            name: request.Name,
            landlineNumber: new LandlineNumber(request.LandlineNumber),
            address: request.Address.ToValueObject()
        );

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
