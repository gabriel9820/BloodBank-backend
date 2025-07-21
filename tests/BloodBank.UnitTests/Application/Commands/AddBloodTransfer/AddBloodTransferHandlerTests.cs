using BloodBank.Application.Commands.AddBloodTransfer;
using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Models;
using BloodBank.UnitTests.Fakers;
using Microsoft.Extensions.Options;

namespace BloodBank.UnitTests.Application.Commands.AddBloodTransfer;

public class AddBloodTransferHandlerTests
{
    private readonly Mock<IBloodTransferRepository> _bloodTransferRepositoryMock;
    private readonly Mock<IHospitalRepository> _hospitalRepositoryMock;
    private readonly Mock<IStockRepository> _stockRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IOptions<StockConfig>> _stockConfigMock;
    private readonly AddBloodTransferHandler _handler;

    public AddBloodTransferHandlerTests()
    {
        _bloodTransferRepositoryMock = new Mock<IBloodTransferRepository>();
        _hospitalRepositoryMock = new Mock<IHospitalRepository>();
        _stockRepositoryMock = new Mock<IStockRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _stockConfigMock = new Mock<IOptions<StockConfig>>();
        _stockConfigMock.Setup(config => config.Value).Returns(new StockConfig { Minimum = 8400 });
        _handler = new AddBloodTransferHandler(
            _bloodTransferRepositoryMock.Object,
            _hospitalRepositoryMock.Object,
            _stockRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _stockConfigMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddTransferAndDecreaseStock_WhenDataIsValidAndHasEnoughStock()
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();
        var existingHospital = new HospitalFaker()
            .RuleFor(h => h.Id, command.HospitalId)
            .Generate();
        var existingStock = new StockFaker()
            .RuleFor(s => s.BloodType, command.BloodType)
            .RuleFor(s => s.RhFactor, command.RhFactor)
            .RuleFor(s => s.QuantityML, command.QuantityML * 2)
            .Generate();
        var expectedQuantity = existingStock.QuantityML - command.QuantityML;

        _bloodTransferRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<BloodTransfer>())).Returns(Task.CompletedTask);
        _hospitalRepositoryMock.Setup(repo => repo.GetByIdAsync(command.HospitalId)).ReturnsAsync(existingHospital);
        _stockRepositoryMock.Setup(repo => repo.GetByBloodTypeAsync(command.BloodType, command.RhFactor)).ReturnsAsync(existingStock);
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        existingStock.QuantityML.Should().Be(expectedQuantity);

        _bloodTransferRepositoryMock.Verify(repo => repo.AddAsync(It.Is<BloodTransfer>(bt =>
            bt.TransferDate == command.TransferDate &&
            bt.BloodType == command.BloodType &&
            bt.RhFactor == command.RhFactor &&
            bt.QuantityML == command.QuantityML &&
            bt.HospitalId == command.HospitalId
        )), Times.Once);
        _hospitalRepositoryMock.Verify(repo => repo.GetByIdAsync(command.HospitalId), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(command.BloodType, command.RhFactor), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenHospitalDoesNotExist()
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();

        _hospitalRepositoryMock.Setup(repo => repo.GetByIdAsync(command.HospitalId)).ReturnsAsync(() => null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(HospitalErrors.HospitalNotFound);

        _bloodTransferRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<BloodTransfer>()), Times.Never);
        _hospitalRepositoryMock.Verify(repo => repo.GetByIdAsync(command.HospitalId), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(command.BloodType, command.RhFactor), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenStockIsInsufficient()
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();
        var existingHospital = new HospitalFaker()
            .RuleFor(h => h.Id, command.HospitalId)
            .Generate();
        var existingStock = new StockFaker()
            .RuleFor(s => s.BloodType, command.BloodType)
            .RuleFor(s => s.RhFactor, command.RhFactor)
            .RuleFor(s => s.QuantityML, command.QuantityML - 1)
            .Generate();

        _hospitalRepositoryMock.Setup(repo => repo.GetByIdAsync(command.HospitalId)).ReturnsAsync(existingHospital);
        _stockRepositoryMock.Setup(repo => repo.GetByBloodTypeAsync(command.BloodType, command.RhFactor)).ReturnsAsync(existingStock);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(StockErrors.InsufficientStock);

        _bloodTransferRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<BloodTransfer>()), Times.Never);
        _hospitalRepositoryMock.Verify(repo => repo.GetByIdAsync(command.HospitalId), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(command.BloodType, command.RhFactor), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenStockDoesNotExist()
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();
        var existingHospital = new HospitalFaker()
            .RuleFor(h => h.Id, command.HospitalId)
            .Generate();

        _hospitalRepositoryMock.Setup(repo => repo.GetByIdAsync(command.HospitalId)).ReturnsAsync(existingHospital);
        _stockRepositoryMock.Setup(repo => repo.GetByBloodTypeAsync(command.BloodType, command.RhFactor)).ReturnsAsync(() => null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(StockErrors.InsufficientStock);

        _bloodTransferRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<BloodTransfer>()), Times.Never);
        _hospitalRepositoryMock.Verify(repo => repo.GetByIdAsync(command.HospitalId), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(command.BloodType, command.RhFactor), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
