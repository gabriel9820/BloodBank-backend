using BloodBank.Application.Queries.GetDonorById;
using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Queries.GetDonorById;

public class GetDonorByIdHandlerTests
{
    private readonly Mock<IDonorRepository> _donorRepositoryMock;
    private readonly GetDonorByIdHandler _getDonorByIdHandler;

    public GetDonorByIdHandlerTests()
    {
        _donorRepositoryMock = new Mock<IDonorRepository>();
        _getDonorByIdHandler = new GetDonorByIdHandler(_donorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnDonorDetailsViewModel_WhenDonorExists()
    {
        // Arrange
        var donorId = 1;
        var query = new GetDonorByIdQuery(donorId);
        var expectedDonor = new DonorFaker()
            .RuleFor(d => d.Id, donorId)
            .Generate();

        _donorRepositoryMock.Setup(dr => dr.GetByIdAsync(donorId)).ReturnsAsync(expectedDonor);

        // Act
        var result = await _getDonorByIdHandler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(donorId);

        _donorRepositoryMock.Verify(dr => dr.GetByIdAsync(donorId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDonorDoesNotExist()
    {
        // Arrange
        var donorId = 1;
        var query = new GetDonorByIdQuery(donorId);

        _donorRepositoryMock.Setup(dr => dr.GetByIdAsync(donorId)).ReturnsAsync(() => null);

        // Act
        var result = await _getDonorByIdHandler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DonorErrors.DonorNotFound);

        _donorRepositoryMock.Verify(dr => dr.GetByIdAsync(donorId), Times.Once);
    }
}
