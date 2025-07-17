using BloodBank.Application.Queries.GetAllDonations;
using BloodBank.Core.Entities;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Queries.GetAllDonations;

public class GetAllDonationsHandlerTests
{
    private readonly Mock<IDonationRepository> _donationRepositoryMock;
    private readonly GetAllDonationsHandler _handler;

    public GetAllDonationsHandlerTests()
    {
        _donationRepositoryMock = new Mock<IDonationRepository>();
        _handler = new GetAllDonationsHandler(_donationRepositoryMock.Object);
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
        var query = new GetAllDonationsQuery { PageNumber = pageNumber, PageSize = pageSize };
        var donations = new DonationFaker().Generate(fakerCount);
        var expectedResult = new PagedResult<Donation>(donations, pageNumber, pageSize, totalRecords, totalPages);

        _donationRepositoryMock.Setup(dr => dr.GetAllAsync(query)).ReturnsAsync(expectedResult);

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

        _donationRepositoryMock.Verify(dr => dr.GetAllAsync(query), Times.Once);
    }
}
