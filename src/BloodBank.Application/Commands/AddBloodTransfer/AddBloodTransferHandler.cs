using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Commands.AddBloodTransfer;

public class AddBloodTransferHandler(
    IBloodTransferRepository bloodTransferRepository,
    IHospitalRepository hospitalRepository,
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AddBloodTransferCommand, Result<int>>
{
    private readonly IBloodTransferRepository _bloodTransferRepository = bloodTransferRepository;
    private readonly IHospitalRepository _hospitalRepository = hospitalRepository;
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<int>> Handle(AddBloodTransferCommand request, CancellationToken cancellationToken)
    {
        var hospital = await _hospitalRepository.GetByIdAsync(request.HospitalId);

        if (hospital is null)
            return HospitalErrors.HospitalNotFound;

        var bloodTransfer = request.ToEntity(hospital);

        var stock = await _stockRepository.GetByBloodTypeAsync(bloodTransfer.BloodType, bloodTransfer.RhFactor);

        if (stock is null || stock.QuantityML < bloodTransfer.QuantityML)
            return StockErrors.InsufficientStock;

        stock.RemoveFromStock(bloodTransfer.QuantityML);

        await _bloodTransferRepository.AddAsync(bloodTransfer);
        await _unitOfWork.SaveChangesAsync();

        return bloodTransfer.Id;
    }
}
