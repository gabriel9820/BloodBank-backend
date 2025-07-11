using BloodBank.Application.Commands.DeleteDonor;
using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Commands.DeleteDonor;

public class DeleteDonorHandlerTests
{
    private readonly Mock<IDonorRepository> _donorRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteDonorHandler _deleteDonorHandler;

    public DeleteDonorHandlerTests()
    {
        _donorRepositoryMock = new Mock<IDonorRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _deleteDonorHandler = new DeleteDonorHandler(_donorRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteDonor_WhenDonorExists()
    {
        // Arrange
        var donorId = 1;
        var command = new DeleteDonorCommand(donorId);
        var expectedDonor = new DonorFaker()
            .RuleFor(d => d.Id, donorId)
            .Generate();

        _donorRepositoryMock.Setup(dr => dr.GetByIdAsync(donorId)).ReturnsAsync(expectedDonor);
        _donorRepositoryMock.Setup(dr => dr.Delete(It.IsAny<Donor>()));
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _deleteDonorHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _donorRepositoryMock.Verify(dr => dr.GetByIdAsync(donorId), Times.Once);
        _donorRepositoryMock.Verify(dr => dr.Delete(It.Is<Donor>(d => d.Id == donorId)), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDonorDoesNotExist()
    {
        // Arrange
        var donorId = 1;
        var command = new DeleteDonorCommand(donorId);

        _donorRepositoryMock.Setup(dr => dr.GetByIdAsync(donorId)).ReturnsAsync(() => null);

        // Act
        var result = await _deleteDonorHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DonorErrors.DonorNotFound);

        _donorRepositoryMock.Verify(dr => dr.GetByIdAsync(donorId), Times.Once);
        _donorRepositoryMock.Verify(dr => dr.Delete(It.IsAny<Donor>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
