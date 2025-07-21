using BloodBank.Application.Events;
using BloodBank.Core.DomainEvents;
using BloodBank.UnitTests.Fakers;
using MediatR;

namespace BloodBank.UnitTests.Application.Events;

public class DomainEventDispatcherTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly DomainEventDispatcher _dispatcher;

    public DomainEventDispatcherTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _dispatcher = new DomainEventDispatcher(_mediatorMock.Object);
    }

    [Fact]
    public async Task DispatchAsync_ShouldPublishAllDomainEvents_WhenEventsArePresent()
    {
        // Arrange
        var domainEvents = new List<IDomainEvent>
        {
            new DomainEventFaker(),
            new DomainEventFaker()
        };

        // Act
        await _dispatcher.DispatchAsync(domainEvents);

        // Assert
        _mediatorMock.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(domainEvents.Count));
    }

    [Fact]
    public async Task DispatchAsync_ShouldNotPublishAnyDomainEvent_WhenEventListIsEmpty()
    {
        // Arrange
        var domainEvents = new List<IDomainEvent>();

        // Act
        await _dispatcher.DispatchAsync(domainEvents);

        // Assert
        _mediatorMock.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }
}
