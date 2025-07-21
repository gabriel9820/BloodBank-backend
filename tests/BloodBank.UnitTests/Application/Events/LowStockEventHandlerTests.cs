using BloodBank.Application.Events;
using BloodBank.Core.DomainEvents;
using BloodBank.Core.Enums;
using BloodBank.Core.Models;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Services.Email;

namespace BloodBank.UnitTests.Application.Events;

public class LowStockEventHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly LowStockEventHandler _handler;

    public LowStockEventHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _emailServiceMock = new Mock<IEmailService>();
        _handler = new LowStockEventHandler(_userRepositoryMock.Object, _emailServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldSendEmail_WhenUsersToNotifyExist()
    {
        // Arrange
        var usersToNotify = new List<UserNotificationDTO>
        {
            new() { Id = 1, Email = "user1@email.com" }
        };
        var lowStockEvent = new LowStockDomainEvent(BloodType.A, RhFactor.Positive, 500);
        var notification = new DomainEventNotification<LowStockDomainEvent>(lowStockEvent);

        _userRepositoryMock.Setup(repo => repo.GetUsersToNotifyLowStockAsync()).ReturnsAsync(usersToNotify);

        // Act
        await _handler.Handle(notification, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(repo => repo.GetUsersToNotifyLowStockAsync(), Times.Once);
        _emailServiceMock.Verify(service => service.SendAsync(
            It.Is<List<string>>(emails => emails.Count == 1 && emails[0] == usersToNotify[0].Email),
            It.Is<string>(subject => subject == "Alerta de Baixo Estoque de Sangue"),
            It.Is<string>(body => body.Contains("A+") && body.Contains("500ml"))
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNotSendEmail_WhenNoUsersToNotify()
    {
        // Arrange
        var lowStockEvent = new LowStockDomainEvent(BloodType.A, RhFactor.Positive, 500);
        var notification = new DomainEventNotification<LowStockDomainEvent>(lowStockEvent);

        _userRepositoryMock.Setup(repo => repo.GetUsersToNotifyLowStockAsync()).ReturnsAsync([]);

        // Act
        await _handler.Handle(notification, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(repo => repo.GetUsersToNotifyLowStockAsync(), Times.Once);
        _emailServiceMock.Verify(service => service.SendAsync(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}
