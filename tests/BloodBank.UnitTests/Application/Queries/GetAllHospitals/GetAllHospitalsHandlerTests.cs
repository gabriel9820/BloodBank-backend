using BloodBank.Application.Queries.GetAllHospitals;
using BloodBank.Core.Entities;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Queries.GetAllHospitals;

public class GetAllHospitalsHandlerTests
{
    private readonly Mock<IHospitalRepository> _hospitalRepositoryMock;
    private readonly GetAllHospitalsHandler _handler;

    public GetAllHospitalsHandlerTests()
    {
        _hospitalRepositoryMock = new Mock<IHospitalRepository>();
        _handler = new GetAllHospitalsHandler(_hospitalRepositoryMock.Object);
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
        var query = new GetAllHospitalsQuery { PageNumber = pageNumber, PageSize = pageSize };
        var hospitals = new HospitalFaker().Generate(fakerCount);
        var expectedResult = new PagedResult<Hospital>(hospitals, pageNumber, pageSize, totalRecords, totalPages);

        _hospitalRepositoryMock.Setup(hr => hr.GetAllAsync(query)).ReturnsAsync(expectedResult);

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

        _hospitalRepositoryMock.Verify(hr => hr.GetAllAsync(query), Times.Once);
    }
}
