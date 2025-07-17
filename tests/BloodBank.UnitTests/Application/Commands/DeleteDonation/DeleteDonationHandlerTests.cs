using BloodBank.Application.Commands.DeleteDonation;
using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Repositories;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Commands.DeleteDonation;

public class DeleteDonationHandlerTests
{
    private readonly Mock<IDonationRepository> _donationRepositoryMock;
    private readonly Mock<IStockRepository> _stockRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteDonationHandler _handler;

    public DeleteDonationHandlerTests()
    {
        _donationRepositoryMock = new Mock<IDonationRepository>();
        _stockRepositoryMock = new Mock<IStockRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteDonationHandler(_donationRepositoryMock.Object, _stockRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteDonationAndRemoveStock_WhenDonationExists()
    {
        // Arrange
        var donationId = 1;
        var command = new DeleteDonationCommand(donationId);
        var existingDonation = new DonationFaker()
            .RuleFor(d => d.Id, donationId)
            .Generate();
        var existingStock = new StockFaker()
            .RuleFor(s => s.BloodType, existingDonation.Donor.BloodType)
            .RuleFor(s => s.RhFactor, existingDonation.Donor.RhFactor)
            .RuleFor(s => s.QuantityML, existingDonation.QuantityML * 2)
            .Generate();
        var expectedQuantity = existingStock.QuantityML - existingDonation.QuantityML;

        _donationRepositoryMock.Setup(repo => repo.GetByIdAsync(donationId)).ReturnsAsync(existingDonation);
        _donationRepositoryMock.Setup(repo => repo.Delete(existingDonation));
        _stockRepositoryMock.Setup(repo => repo.GetByBloodTypeAsync(existingDonation.Donor.BloodType, existingDonation.Donor.RhFactor)).ReturnsAsync(existingStock);
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        existingStock.QuantityML.Should().Be(expectedQuantity);

        _donationRepositoryMock.Verify(repo => repo.GetByIdAsync(donationId), Times.Once);
        _donationRepositoryMock.Verify(repo => repo.Delete(existingDonation), Times.Once);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(existingDonation.Donor.BloodType, existingDonation.Donor.RhFactor), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDonationDoesNotExist()
    {
        // Arrange
        var donationId = 1;
        var command = new DeleteDonationCommand(donationId);

        _donationRepositoryMock.Setup(repo => repo.GetByIdAsync(donationId)).ReturnsAsync(() => null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DonationErrors.DonationNotFound);

        _donationRepositoryMock.Verify(repo => repo.GetByIdAsync(donationId), Times.Once);
        _donationRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Donation>()), Times.Never);
        _stockRepositoryMock.Verify(repo => repo.GetByBloodTypeAsync(It.IsAny<BloodType>(), It.IsAny<RhFactor>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
