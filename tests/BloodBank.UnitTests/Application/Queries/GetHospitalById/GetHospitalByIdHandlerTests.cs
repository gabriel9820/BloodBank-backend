using BloodBank.Application.Queries.GetHospitalById;
using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Queries.GetHospitalById;

public class GetHospitalByIdHandlerTests
{
    private readonly Mock<IHospitalRepository> _hospitalRepositoryMock;
    private readonly GetHospitalByIdHandler _getHospitalByIdHandler;

    public GetHospitalByIdHandlerTests()
    {
        _hospitalRepositoryMock = new Mock<IHospitalRepository>();
        _getHospitalByIdHandler = new GetHospitalByIdHandler(_hospitalRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnHospitalDetailsViewModel_WhenHospitalExists()
    {
        // Arrange
        var hospitalId = 1;
        var query = new GetHospitalByIdQuery(hospitalId);
        var expectedHospital = new HospitalFaker()
            .RuleFor(h => h.Id, hospitalId)
            .Generate();

        _hospitalRepositoryMock.Setup(hr => hr.GetByIdAsync(hospitalId)).ReturnsAsync(expectedHospital);

        // Act
        var result = await _getHospitalByIdHandler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(expectedHospital.Id);

        _hospitalRepositoryMock.Verify(hr => hr.GetByIdAsync(hospitalId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenHospitalDoesNotExist()
    {
        // Arrange
        var hospitalId = 1;
        var query = new GetHospitalByIdQuery(hospitalId);

        _hospitalRepositoryMock.Setup(hr => hr.GetByIdAsync(hospitalId)).ReturnsAsync(() => null);

        // Act
        var result = await _getHospitalByIdHandler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(HospitalErrors.HospitalNotFound);

        _hospitalRepositoryMock.Verify(hr => hr.GetByIdAsync(hospitalId), Times.Once);
    }
}
