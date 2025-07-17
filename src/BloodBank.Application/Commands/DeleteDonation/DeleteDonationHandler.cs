using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Commands.DeleteDonation;

public class DeleteDonationHandler(
    IDonationRepository donationRepository,
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteDonationCommand, Result>
{
    private readonly IDonationRepository _donationRepository = donationRepository;
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteDonationCommand request, CancellationToken cancellationToken)
    {
        var donation = await _donationRepository.GetByIdAsync(request.Id);

        if (donation is null)
            return DonationErrors.DonationNotFound;

        var stock = await _stockRepository.GetByBloodTypeAsync(donation.Donor.BloodType, donation.Donor.RhFactor);
        stock?.RemoveFromStock(donation.QuantityML);

        _donationRepository.Delete(donation);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
