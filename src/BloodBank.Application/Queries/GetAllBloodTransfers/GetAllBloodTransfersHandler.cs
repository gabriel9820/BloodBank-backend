using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Queries.GetAllBloodTransfers;

public class GetAllBloodTransfersHandler(
    IBloodTransferRepository bloodTransferRepository) : IRequestHandler<GetAllBloodTransfersQuery, Result<PagedResult<BloodTransferListViewModel>>>
{
    private readonly IBloodTransferRepository _bloodTransferRepository = bloodTransferRepository;

    public async Task<Result<PagedResult<BloodTransferListViewModel>>> Handle(GetAllBloodTransfersQuery request, CancellationToken cancellationToken)
    {
        var pagedTransfers = await _bloodTransferRepository.GetAllAsync(request);

        return new PagedResult<BloodTransferListViewModel>(
            pagedTransfers.Data.Select(bt => bt.ToListViewModel()),
            pagedTransfers.PageNumber,
            pagedTransfers.PageSize,
            pagedTransfers.TotalRecords,
            pagedTransfers.TotalPages
        );
    }
}
