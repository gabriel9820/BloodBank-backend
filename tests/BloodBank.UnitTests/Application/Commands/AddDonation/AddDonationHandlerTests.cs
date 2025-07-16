using BloodBank.Application.Commands.AddDonation;
using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Commands.AddDonation;

public class AddDonationHandlerTests
{
    private readonly Mock<IDonationRepository> _donationRepositoryMock;
    private readonly Mock<IDonorRepository> _donorRepositoryMock;
    private readonly Mock<IStockRepository> _stockRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AddDonationHandler _handler;

    public AddDonationHandlerTests()
    {
        _donationRepositoryMock = new Mock<IDonationRepository>();
        _donorRepositoryMock = new Mock<IDonorRepository>();
        _stockRepositoryMock = new Mock<IStockRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new AddDonationHandler(
            _donationRepositoryMock.Object,
            _donorRepositoryMock.Object,
            _stockRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldAddDonationAndUpdateStock_WhenDataIsValidAndStockExists()
    {
        // Arrange
        var command = new AddDonationCommandFaker().Generate();
        var existingDonor = new DonorFaker()
            .RuleFor(d => d.Id, command.DonorId)
            .Generate();
        var existingStock = new StockFaker()
            .RuleFor(s => s.BloodType, existingDonor.BloodType)
            .RuleFor(s => s.RhFactor, existingDonor.RhFactor)
            .Generate();
        var expectedQuantity = existingStock.QuantityML + command.QuantityML;
        var lastDonationDate = DateTime.UtcNow.AddYears(-1);

        _donationRepositoryMock.Setup(repo => repo.GetLastDonationDateByDonorIdAsync(command.DonorId)).ReturnsAsync(lastDonationDate);
        _donationRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Donation>())).Returns(Task.CompletedTask);
        _donorRepositoryMock.Setup(repo => repo.GetByIdAsync(command.DonorId)).ReturnsAsync(existingDonor);
        _stockRepositoryMock.Setup(repo => repo.GetByBloodTypeAsync(existingDonor.BloodType, existingDonor.RhFactor)).ReturnsAsync(existingStock);
        _stockRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Stock>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        existingStock.QuantityML.Should().Be(expectedQuantity);

        _donationRepositoryMock.Verify(repo => repo.GetLastDonationDateByDonorIdAsync(command.DonorId), Times.Once);
        _donationRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Donation>(d =>
            d.DonationDate == command.DonationDate &&
            d.QuantityML == command.QuantityML &&
            d.DonorId == command.DonorId
        )), Times.Once);
        _donorRepositoryMock.Verify(repo => repo.GetByIdAsync(command.DonorId), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(existingDonor.BloodType, existingDonor.RhFactor), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Stock>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldAddDonationAndCreateStock_WhenDataIsValidAndStockDoesNotExist()
    {
        // Arrange
        var command = new AddDonationCommandFaker().Generate();
        var existingDonor = new DonorFaker()
            .RuleFor(d => d.Id, command.DonorId)
            .Generate();
        Stock? createdStock = null;
        var expectedQuantity = command.QuantityML;
        var lastDonationDate = DateTime.UtcNow.AddYears(-1);

        _donationRepositoryMock.Setup(repo => repo.GetLastDonationDateByDonorIdAsync(command.DonorId)).ReturnsAsync(lastDonationDate);
        _donationRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Donation>())).Returns(Task.CompletedTask);
        _donorRepositoryMock.Setup(repo => repo.GetByIdAsync(command.DonorId)).ReturnsAsync(existingDonor);
        _stockRepositoryMock.Setup(repo => repo.GetByBloodTypeAsync(existingDonor.BloodType, existingDonor.RhFactor)).ReturnsAsync(() => null);
        _stockRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Stock>())).Callback<Stock>(stock => createdStock = stock).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        createdStock!.QuantityML.Should().Be(expectedQuantity);

        _donationRepositoryMock.Verify(repo => repo.GetLastDonationDateByDonorIdAsync(command.DonorId), Times.Once);
        _donationRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Donation>(d =>
            d.DonationDate == command.DonationDate &&
            d.QuantityML == command.QuantityML &&
            d.DonorId == command.DonorId
        )), Times.Once);
        _donorRepositoryMock.Verify(repo => repo.GetByIdAsync(command.DonorId), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(existingDonor.BloodType, existingDonor.RhFactor), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Stock>(s =>
            s.BloodType == existingDonor.BloodType &&
            s.RhFactor == existingDonor.RhFactor &&
            s.QuantityML == expectedQuantity
        )), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDonorNotFound()
    {
        // Arrange
        var command = new AddDonationCommandFaker().Generate();

        _donorRepositoryMock.Setup(repo => repo.GetByIdAsync(command.DonorId)).ReturnsAsync(() => null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DonorErrors.DonorNotFound);

        _donationRepositoryMock.Verify(repo => repo.GetLastDonationDateByDonorIdAsync(It.IsAny<int>()), Times.Never);
        _donationRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Donation>()), Times.Never);
        _donorRepositoryMock.Verify(repo => repo.GetByIdAsync(command.DonorId), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(It.IsAny<BloodType>(), It.IsAny<RhFactor>()), Times.Never);
        _stockRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Stock>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDonorCannotDonate()
    {
        // Arrange
        var command = new AddDonationCommandFaker().Generate();
        var existingDonor = new DonorFaker()
            .RuleFor(d => d.Id, command.DonorId)
            .Generate();
        var lastDonationDate = DateTime.UtcNow;

        _donationRepositoryMock.Setup(repo => repo.GetLastDonationDateByDonorIdAsync(command.DonorId)).ReturnsAsync(lastDonationDate);
        _donorRepositoryMock.Setup(repo => repo.GetByIdAsync(command.DonorId)).ReturnsAsync(existingDonor);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DonorErrors.DonorCannotDonate);

        _donationRepositoryMock.Verify(repo => repo.GetLastDonationDateByDonorIdAsync(command.DonorId), Times.Once);
        _donationRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Donation>()), Times.Never);
        _donorRepositoryMock.Verify(repo => repo.GetByIdAsync(command.DonorId), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(It.IsAny<BloodType>(), It.IsAny<RhFactor>()), Times.Never);
        _stockRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Stock>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
