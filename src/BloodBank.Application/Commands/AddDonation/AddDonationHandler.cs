using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Commands.AddDonation;

public class AddDonationHandler(
    IDonationRepository donationRepository,
    IDonorRepository donorRepository,
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AddDonationCommand, Result<int>>
{
    private readonly IDonationRepository _donationRepository = donationRepository;
    private readonly IDonorRepository _donorRepository = donorRepository;
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<int>> Handle(AddDonationCommand request, CancellationToken cancellationToken)
    {
        var donor = await _donorRepository.GetByIdAsync(request.DonorId);

        if (donor is null)
            return DonorErrors.DonorNotFound;

        var lastDonationDate = await _donationRepository.GetLastDonationDateByDonorIdAsync(donor.Id);

        if (!donor.CanDonate(lastDonationDate))
            return DonorErrors.DonorCannotDonate;

        var donation = request.ToEntity(donor);

        var stock = await _stockRepository.GetByBloodTypeAsync(donor.BloodType, donor.RhFactor);

        if (stock is null)
        {
            stock = new Stock(donor.BloodType, donor.RhFactor, 0);
            await _stockRepository.AddAsync(stock);
        }

        stock.AddToStock(donation.QuantityML);

        await _donationRepository.AddAsync(donation);
        await _unitOfWork.SaveChangesAsync();

        return donation.Id;
    }
}
