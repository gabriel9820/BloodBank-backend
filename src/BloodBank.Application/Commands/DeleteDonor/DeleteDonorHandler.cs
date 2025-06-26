using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Commands.DeleteDonor;

public class DeleteDonorHandler(
    IDonorRepository donorRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteDonorCommand, Result>
{
    private readonly IDonorRepository _donorRepository = donorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteDonorCommand request, CancellationToken cancellationToken)
    {
        var donor = await _donorRepository.GetByIdAsync(request.Id);

        if (donor is null)
            return DonorErrors.DonorNotFound;

        _donorRepository.Delete(donor);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
