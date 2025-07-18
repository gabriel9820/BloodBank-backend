using BloodBank.Application.Queries.GetAllStocks;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Queries.GetAllStocks;

public class GetAllStocksHandlerTests
{
    private readonly Mock<IStockRepository> _stockRepositoryMock;
    private readonly GetAllStocksHandler _handler;

    public GetAllStocksHandlerTests()
    {
        _stockRepositoryMock = new Mock<IStockRepository>();
        _handler = new GetAllStocksHandler(_stockRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllStocksAsViewModel()
    {
        // Arrange
        var count = 2;
        var query = new GetAllStocksQuery();
        var stocks = new StockFaker().Generate(count);

        _stockRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(stocks);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Count().Should().Be(count);

        _stockRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }
}
