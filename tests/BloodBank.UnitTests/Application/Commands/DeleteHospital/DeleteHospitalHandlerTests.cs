using BloodBank.Application.Commands.DeleteHospital;
using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Commands.DeleteHospital;

public class DeleteHospitalHandlerTests
{
    private readonly Mock<IHospitalRepository> _hospitalRepositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly DeleteHospitalHandler _deleteHospitalHandler;

    public DeleteHospitalHandlerTests()
    {
        _hospitalRepositoryMock = new Mock<IHospitalRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _deleteHospitalHandler = new DeleteHospitalHandler(_hospitalRepositoryMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteHospital_WhenHospitalExists()
    {
        // Arrange
        var hospitalId = 1;
        var command = new DeleteHospitalCommand(hospitalId);
        var expectedHospital = new HospitalFaker()
            .RuleFor(h => h.Id, hospitalId)
            .Generate();

        _hospitalRepositoryMock.Setup(hr => hr.GetByIdAsync(hospitalId)).ReturnsAsync(expectedHospital);
        _hospitalRepositoryMock.Setup(hr => hr.Delete(It.IsAny<Hospital>()));
        _uowMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _deleteHospitalHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _hospitalRepositoryMock.Verify(hr => hr.GetByIdAsync(hospitalId), Times.Once);
        _hospitalRepositoryMock.Verify(hr => hr.Delete(It.Is<Hospital>(h => h.Id == hospitalId)), Times.Once);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenHospitalDoesNotExist()
    {
        // Arrange
        var hospitalId = 1;
        var command = new DeleteHospitalCommand(hospitalId);

        _hospitalRepositoryMock.Setup(hr => hr.GetByIdAsync(hospitalId)).ReturnsAsync(() => null);

        // Act
        var result = await _deleteHospitalHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(HospitalErrors.HospitalNotFound);

        _hospitalRepositoryMock.Verify(hr => hr.GetByIdAsync(hospitalId), Times.Once);
        _hospitalRepositoryMock.Verify(hr => hr.Delete(It.IsAny<Hospital>()), Times.Never);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
