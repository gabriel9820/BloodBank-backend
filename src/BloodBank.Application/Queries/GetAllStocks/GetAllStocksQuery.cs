using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using MediatR;

namespace BloodBank.Application.Queries.GetAllStocks;

public class GetAllStocksQuery : IRequest<Result<IEnumerable<StockListViewModel>>>
{

}
