using BloodBank.Application.Queries.GetAllBloodTransfers;
using BloodBank.Core.Entities;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Queries.GetAllBloodTransfers;

public class GetAllBloodTransfersHandlerTests
{
    private readonly Mock<IBloodTransferRepository> _bloodTransferRepositoryMock;
    private readonly GetAllBloodTransfersHandler _handler;

    public GetAllBloodTransfersHandlerTests()
    {
        _bloodTransferRepositoryMock = new Mock<IBloodTransferRepository>();
        _handler = new GetAllBloodTransfersHandler(_bloodTransferRepositoryMock.Object);
    }

    [Theory]
    [InlineData(1, 10, 20, 2, 10)]
    [InlineData(2, 5, 10, 2, 5)]
    [InlineData(1, 10, 0, 0, 0)]
    public async Task Handle_ShouldReturnPagedResult(
        int pageNumber,
        int pageSize,
        int totalRecords,
        int totalPages,
        int fakerCount)
    {
        // Arrange
        var query = new GetAllBloodTransfersQuery { PageNumber = pageNumber, PageSize = pageSize };
        var bloodTransfers = new BloodTransferFaker().Generate(fakerCount);
        var expectedResult = new PagedResult<BloodTransfer>(bloodTransfers, pageNumber, pageSize, totalRecords, totalPages);

        _bloodTransferRepositoryMock.Setup(dr => dr.GetAllAsync(query)).ReturnsAsync(expectedResult);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.PageNumber.Should().Be(pageNumber);
        result.Data.PageSize.Should().Be(pageSize);
        result.Data.TotalRecords.Should().Be(totalRecords);
        result.Data.TotalPages.Should().Be(totalPages);
        result.Data.Data.Should().HaveCount(fakerCount);

        _bloodTransferRepositoryMock.Verify(dr => dr.GetAllAsync(query), Times.Once);
    }
}
