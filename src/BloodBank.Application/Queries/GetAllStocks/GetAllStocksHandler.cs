using BloodBank.Application.DTOs.ViewModels;
using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using MediatR;

namespace BloodBank.Application.Queries.GetAllStocks;

public class GetAllStocksHandler(
    IStockRepository stockRepository) : IRequestHandler<GetAllStocksQuery, Result<IEnumerable<StockListViewModel>>>
{
    private readonly IStockRepository _stockRepository = stockRepository;

    public async Task<Result<IEnumerable<StockListViewModel>>> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
    {
        var stocks = await _stockRepository.GetAllAsync();
        return stocks.Select(stock => stock.ToListViewModel()).ToList();
    }
}
