using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Options;

namespace BloodBank.Application.Commands.AddBloodTransfer;

public class AddBloodTransferHandler(
    IBloodTransferRepository bloodTransferRepository,
    IHospitalRepository hospitalRepository,
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork,
    IOptions<StockConfig> stockConfig) : IRequestHandler<AddBloodTransferCommand, Result<int>>
{
    private readonly IBloodTransferRepository _bloodTransferRepository = bloodTransferRepository;
    private readonly IHospitalRepository _hospitalRepository = hospitalRepository;
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly StockConfig _stockConfig = stockConfig.Value;

    public async Task<Result<int>> Handle(AddBloodTransferCommand request, CancellationToken cancellationToken)
    {
        var hospital = await _hospitalRepository.GetByIdAsync(request.HospitalId);

        if (hospital is null)
            return HospitalErrors.HospitalNotFound;

        var bloodTransfer = request.ToEntity(hospital);

        var stock = await _stockRepository.GetByBloodTypeAsync(bloodTransfer.BloodType, bloodTransfer.RhFactor);

        if (stock is null || stock.QuantityML < bloodTransfer.QuantityML)
            return StockErrors.InsufficientStock;

        stock.RemoveFromStock(bloodTransfer.QuantityML, _stockConfig.Minimum);

        await _bloodTransferRepository.AddAsync(bloodTransfer);
        await _unitOfWork.SaveChangesAsync();

        return bloodTransfer.Id;
    }
}
