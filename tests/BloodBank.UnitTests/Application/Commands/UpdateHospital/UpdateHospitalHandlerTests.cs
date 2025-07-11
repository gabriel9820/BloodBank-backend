using BloodBank.Application.Commands.UpdateHospital;
using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Commands.UpdateHospital;

public class UpdateHospitalHandlerTests
{
    private readonly Mock<IHospitalRepository> _hospitalRepositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly UpdateHospitalHandler _updateHospitalHandler;

    public UpdateHospitalHandlerTests()
    {
        _hospitalRepositoryMock = new Mock<IHospitalRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _updateHospitalHandler = new UpdateHospitalHandler(_hospitalRepositoryMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateHospital_WhenHospitalExists()
    {
        // Arrange
        var command = new UpdateHospitalCommandFaker().Generate();
        var existingHospital = new HospitalFaker()
            .RuleFor(h => h.Id, command.Id)
            .Generate();

        _hospitalRepositoryMock.Setup(hr => hr.GetByIdAsync(command.Id)).ReturnsAsync(existingHospital);
        _uowMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _updateHospitalHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _hospitalRepositoryMock.Verify(hr => hr.GetByIdAsync(command.Id), Times.Once);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenHospitalDoesNotExist()
    {
        // Arrange
        var command = new UpdateHospitalCommandFaker().Generate();

        _hospitalRepositoryMock.Setup(hr => hr.GetByIdAsync(command.Id)).ReturnsAsync(() => null);

        // Act
        var result = await _updateHospitalHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(HospitalErrors.HospitalNotFound);

        _hospitalRepositoryMock.Verify(hr => hr.GetByIdAsync(command.Id), Times.Once);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
