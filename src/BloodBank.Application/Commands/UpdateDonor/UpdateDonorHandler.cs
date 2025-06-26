using BloodBank.Application.DTOs.InputModels;
using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using BloodBank.Core.ValueObjects;
using MediatR;

namespace BloodBank.Application.Commands.UpdateDonor;

public class UpdateDonorHandler(
    IDonorRepository donorRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateDonorCommand, Result>
{
    private readonly IDonorRepository _donorRepository = donorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateDonorCommand request, CancellationToken cancellationToken)
    {
        var donor = await _donorRepository.GetByIdAsync(request.Id);

        if (donor is null)
            return DonorErrors.DonorNotFound;

        donor.Update(
            fullName: request.FullName,
            cellPhoneNumber: new CellPhoneNumber(request.CellPhoneNumber),
            email: new Email(request.Email),
            birthDate: request.BirthDate,
            gender: request.Gender,
            weight: request.Weight,
            bloodType: request.BloodType,
            rhFactor: request.RhFactor,
            address: request.Address.ToValueObject()
        );

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
