using BloodBank.Application.Queries.GetAllDonors;
using BloodBank.Core.Entities;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Queries.GetAllDonors;

public class GetAllDonorsHandlerTests
{
    private readonly Mock<IDonorRepository> _donorRepositoryMock;
    private readonly GetAllDonorsHandler _handler;

    public GetAllDonorsHandlerTests()
    {
        _donorRepositoryMock = new Mock<IDonorRepository>();
        _handler = new GetAllDonorsHandler(_donorRepositoryMock.Object);
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
        var query = new GetAllDonorsQuery { PageNumber = pageNumber, PageSize = pageSize };
        var donors = new DonorFaker().Generate(fakerCount);
        var expectedResult = new PagedResult<Donor>(donors, pageNumber, pageSize, totalRecords, totalPages);

        _donorRepositoryMock.Setup(dr => dr.GetAllAsync(query)).ReturnsAsync(expectedResult);

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

        _donorRepositoryMock.Verify(dr => dr.GetAllAsync(query), Times.Once);
    }
}
