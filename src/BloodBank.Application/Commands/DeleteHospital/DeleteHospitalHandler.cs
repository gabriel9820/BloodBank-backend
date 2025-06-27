using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Commands.DeleteHospital;

public class DeleteHospitalHandler(
    IHospitalRepository hospitalRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteHospitalCommand, Result>
{
    private readonly IHospitalRepository _hospitalRepository = hospitalRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteHospitalCommand request, CancellationToken cancellationToken)
    {
        var hospital = await _hospitalRepository.GetByIdAsync(request.Id);

        if (hospital is null)
            return HospitalErrors.HospitalNotFound;

        _hospitalRepository.Delete(hospital);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
