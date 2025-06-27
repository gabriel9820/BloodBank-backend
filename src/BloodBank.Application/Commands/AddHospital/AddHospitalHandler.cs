using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Commands.AddHospital;

public class AddHospitalHandler(
    IHospitalRepository hospitalRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AddHospitalCommand, Result<int>>
{
    private readonly IHospitalRepository _hospitalRepository = hospitalRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<int>> Handle(AddHospitalCommand request, CancellationToken cancellationToken)
    {
        var hospital = request.ToEntity();

        await _hospitalRepository.AddAsync(hospital);
        await _unitOfWork.SaveChangesAsync();

        return hospital.Id;
    }
}
