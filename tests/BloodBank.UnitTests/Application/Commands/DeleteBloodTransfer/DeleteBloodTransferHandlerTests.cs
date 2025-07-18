using BloodBank.Application.Commands.DeleteBloodTransfer;
using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Commands.DeleteBloodTransfer;

public class DeleteBloodTransferHandlerTests
{
    private readonly Mock<IBloodTransferRepository> _bloodTransferRepositoryMock;
    private readonly Mock<IStockRepository> _stockRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteBloodTransferHandler _handler;

    public DeleteBloodTransferHandlerTests()
    {
        _bloodTransferRepositoryMock = new Mock<IBloodTransferRepository>();
        _stockRepositoryMock = new Mock<IStockRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteBloodTransferHandler(
            _bloodTransferRepositoryMock.Object,
            _stockRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldDeleteBloodTransferAndIncreaseStock_WhenTransferExists()
    {
        // Arrange
        var transferId = 1;
        var command = new DeleteBloodTransferCommand(transferId);
        var existingTranfer = new BloodTransferFaker()
            .RuleFor(bt => bt.Id, transferId)
            .Generate();
        var existingStock = new StockFaker()
            .RuleFor(s => s.BloodType, existingTranfer.BloodType)
            .RuleFor(s => s.RhFactor, existingTranfer.RhFactor)
            .Generate();
        var expectedQuantity = existingStock.QuantityML + existingTranfer.QuantityML;

        _bloodTransferRepositoryMock.Setup(repo => repo.GetByIdAsync(transferId)).ReturnsAsync(existingTranfer);
        _bloodTransferRepositoryMock.Setup(repo => repo.Delete(existingTranfer));
        _stockRepositoryMock.Setup(repo => repo.GetByBloodTypeAsync(existingTranfer.BloodType, existingTranfer.RhFactor)).ReturnsAsync(existingStock);
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        existingStock.QuantityML.Should().Be(expectedQuantity);

        _bloodTransferRepositoryMock.Verify(repo => repo.GetByIdAsync(transferId), Times.Once);
        _bloodTransferRepositoryMock.Verify(repo => repo.Delete(existingTranfer), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(existingTranfer.BloodType, existingTranfer.RhFactor), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenTransferDoesNotExist()
    {
        // Arrange
        var transferId = 1;
        var command = new DeleteBloodTransferCommand(transferId);

        _bloodTransferRepositoryMock.Setup(repo => repo.GetByIdAsync(transferId)).ReturnsAsync(() => null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(BloodTransferErrors.BloodTransferNotFound);

        _bloodTransferRepositoryMock.Verify(repo => repo.GetByIdAsync(transferId), Times.Once);
        _bloodTransferRepositoryMock.Verify(repo => repo.Delete(It.IsAny<BloodTransfer>()), Times.Never);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(It.IsAny<BloodType>(), It.IsAny<RhFactor>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenStockDoesNotExist()
    {
        // Arrange
        var transferId = 1;
        var command = new DeleteBloodTransferCommand(transferId);
        var existingTranfer = new BloodTransferFaker()
            .RuleFor(bt => bt.Id, transferId)
            .Generate();

        _bloodTransferRepositoryMock.Setup(repo => repo.GetByIdAsync(transferId)).ReturnsAsync(existingTranfer);
        _stockRepositoryMock.Setup(repo => repo.GetByBloodTypeAsync(existingTranfer.BloodType, existingTranfer.RhFactor)).ReturnsAsync(() => null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(StockErrors.StockNotFound);

        _bloodTransferRepositoryMock.Verify(repo => repo.GetByIdAsync(transferId), Times.Once);
        _bloodTransferRepositoryMock.Verify(repo => repo.Delete(It.IsAny<BloodTransfer>()), Times.Never);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(existingTranfer.BloodType, existingTranfer.RhFactor), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
