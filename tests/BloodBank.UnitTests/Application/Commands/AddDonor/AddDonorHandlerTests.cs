using BloodBank.Application.Commands.AddDonor;
using BloodBank.Application.DTOs.InputModels;
using BloodBank.Application.Results;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Core.ValueObjects;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Commands.AddDonor;

public class AddDonorHandlerTests
{
    private readonly Mock<IDonorRepository> _donorRepositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly AddDonorHandler _addDonorHandler;

    public AddDonorHandlerTests()
    {
        _donorRepositoryMock = new Mock<IDonorRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _addDonorHandler = new AddDonorHandler(_donorRepositoryMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddDonor_WhenDataIsValid()
    {
        // Arrange
        var command = new AddDonorCommandFaker().Generate();

        _donorRepositoryMock.Setup(dr => dr.IsEmailInUseAsync(command.Email)).ReturnsAsync(false);
        _donorRepositoryMock.Setup(dr => dr.IsCellPhoneNumberInUseAsync(command.CellPhoneNumber)).ReturnsAsync(false);
        _donorRepositoryMock.Setup(dr => dr.AddAsync(It.IsAny<Donor>())).Returns(Task.CompletedTask);
        _uowMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _addDonorHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _donorRepositoryMock.Verify(dr => dr.IsEmailInUseAsync(command.Email), Times.Once);
        _donorRepositoryMock.Verify(dr => dr.IsCellPhoneNumberInUseAsync(command.CellPhoneNumber), Times.Once);
        _donorRepositoryMock.Verify(dr => dr.AddAsync(It.Is<Donor>(d =>
            d.FullName == command.FullName &&
            d.CellPhoneNumber.Equals(new CellPhoneNumber(command.CellPhoneNumber)) &&
            d.Email.Equals(new Email(command.Email)) &&
            d.BirthDate == command.BirthDate &&
            d.Gender == command.Gender &&
            d.Weight == command.Weight &&
            d.BloodType == command.BloodType &&
            d.RhFactor == command.RhFactor &&
            d.Address.Equals(command.Address.ToValueObject())
        )), Times.Once);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenEmailIsInUse()
    {
        // Arrange
        var command = new AddDonorCommandFaker().Generate();

        _donorRepositoryMock.Setup(dr => dr.IsEmailInUseAsync(command.Email)).ReturnsAsync(true);

        // Act
        var result = await _addDonorHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DonorErrors.EmailAlreadyInUse);

        _donorRepositoryMock.Verify(dr => dr.IsEmailInUseAsync(command.Email), Times.Once);
        _donorRepositoryMock.Verify(dr => dr.IsCellPhoneNumberInUseAsync(It.IsAny<string>()), Times.Never);
        _donorRepositoryMock.Verify(dr => dr.AddAsync(It.IsAny<Donor>()), Times.Never);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCellPhoneNumberIsInUse()
    {
        // Arrange
        var command = new AddDonorCommandFaker().Generate();

        _donorRepositoryMock.Setup(dr => dr.IsEmailInUseAsync(command.Email)).ReturnsAsync(false);
        _donorRepositoryMock.Setup(dr => dr.IsCellPhoneNumberInUseAsync(command.CellPhoneNumber)).ReturnsAsync(true);

        // Act
        var result = await _addDonorHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DonorErrors.CellPhoneNumberAlreadyInUse);

        _donorRepositoryMock.Verify(dr => dr.IsEmailInUseAsync(command.Email), Times.Once);
        _donorRepositoryMock.Verify(dr => dr.IsCellPhoneNumberInUseAsync(It.IsAny<string>()), Times.Once);
        _donorRepositoryMock.Verify(dr => dr.AddAsync(It.IsAny<Donor>()), Times.Never);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
