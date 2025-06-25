using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Commands.AddDonor;

public class AddDonorHandler(
    IDonorRepository donorRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AddDonorCommand, Result<int>>
{
    private readonly IDonorRepository _donorRepository = donorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<int>> Handle(AddDonorCommand request, CancellationToken cancellationToken)
    {
        if (await _donorRepository.IsEmailInUseAsync(request.Email))
            return DonorErrors.EmailAlreadyInUse;

        if (await _donorRepository.IsCellPhoneNumberInUseAsync(request.CellPhoneNumber))
            return DonorErrors.CellPhoneNumberAlreadyInUse;

        var donor = request.ToEntity();

        await _donorRepository.AddAsync(donor);
        await _unitOfWork.SaveChangesAsync();

        return donor.Id;
    }
}
