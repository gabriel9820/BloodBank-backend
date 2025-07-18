using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Models;
using MediatR;

namespace BloodBank.Application.Queries.GetAllBloodTransfers;

public class GetAllBloodTransfersQuery : BloodTransferPagedRequest, IRequest<Result<PagedResult<BloodTransferListViewModel>>>
{

}
