using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Commands.DeleteBloodTransfer;

public class DeleteBloodTransferHandler(
    IBloodTransferRepository bloodTransferRepository,
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteBloodTransferCommand, Result>
{
    private readonly IBloodTransferRepository _bloodTransferRepository = bloodTransferRepository;
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteBloodTransferCommand request, CancellationToken cancellationToken)
    {
        var bloodTransfer = await _bloodTransferRepository.GetByIdAsync(request.Id);

        if (bloodTransfer is null)
            return BloodTransferErrors.BloodTransferNotFound;

        var stock = await _stockRepository.GetByBloodTypeAsync(bloodTransfer.BloodType, bloodTransfer.RhFactor);

        if (stock is null)
            return StockErrors.StockNotFound;

        stock.AddToStock(bloodTransfer.QuantityML);

        _bloodTransferRepository.Delete(bloodTransfer);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
