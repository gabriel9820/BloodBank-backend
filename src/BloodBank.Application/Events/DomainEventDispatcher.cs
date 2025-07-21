using BloodBank.Core.DomainEvents;
using MediatR;

namespace BloodBank.Application.Events;

public class DomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    private readonly IMediator _mediator = mediator;

    public async Task DispatchAsync(IEnumerable<IDomainEvent> events)
    {
        foreach (var domainEvent in events)
        {
            var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
            var notification = Activator.CreateInstance(notificationType, domainEvent);
            await _mediator.Publish((INotification)notification!);
        }
    }
}
