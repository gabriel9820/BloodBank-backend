using BloodBank.Application.Commands.AddHospital;
using BloodBank.Application.DTOs.InputModels;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Core.ValueObjects;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Application.Commands.AddHospital;

public class AddHospitalHandlerTests
{
    private readonly Mock<IHospitalRepository> _hospitalRepositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly AddHospitalHandler _addHospitalHandler;

    public AddHospitalHandlerTests()
    {
        _hospitalRepositoryMock = new Mock<IHospitalRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _addHospitalHandler = new AddHospitalHandler(_hospitalRepositoryMock.Object, _uowMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddHospital_WhenCommandIsValid()
    {
        // Arrange
        var command = new AddHospitalCommandFaker().Generate();

        _hospitalRepositoryMock.Setup(hr => hr.AddAsync(It.IsAny<Hospital>())).Returns(Task.CompletedTask);
        _uowMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _addHospitalHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _hospitalRepositoryMock.Verify(hr => hr.AddAsync(It.Is<Hospital>(h =>
            h.Name == command.Name &&
            h.LandlineNumber.Equals(new LandlineNumber(command.LandlineNumber)) &&
            h.Address.Equals(command.Address.ToValueObject())
        )), Times.Once);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }
}
