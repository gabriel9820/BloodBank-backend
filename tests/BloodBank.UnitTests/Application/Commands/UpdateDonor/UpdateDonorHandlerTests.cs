using BloodBank.Application.Commands.UpdateDonor;
using BloodBank.Application.Results;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Commands.UpdateDonor;

public class UpdateDonorHandlerTests
{
    private readonly Mock<IDonorRepository> _donorRepositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly UpdateDonorHandler _updateDonorHandler;

    public UpdateDonorHandlerTests()
    {
        _donorRepositoryMock = new Mock<IDonorRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _updateDonorHandler = new UpdateDonorHandler(_donorRepositoryMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateDonor_WhenDonorExists()
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();
        var existingDonor = new DonorFaker()
            .RuleFor(d => d.Id, command.Id)
            .Generate();

        _donorRepositoryMock.Setup(dr => dr.GetByIdAsync(command.Id)).ReturnsAsync(existingDonor);
        _uowMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _updateDonorHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _donorRepositoryMock.Verify(dr => dr.GetByIdAsync(command.Id), Times.Once);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDonorDoesNotExist()
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();

        _donorRepositoryMock.Setup(dr => dr.GetByIdAsync(command.Id)).ReturnsAsync(() => null);

        // Act
        var result = await _updateDonorHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DonorErrors.DonorNotFound);

        _donorRepositoryMock.Verify(dr => dr.GetByIdAsync(command.Id), Times.Once);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
