using BloodBank.Core.DomainEvents;
using BloodBank.Core.Enums;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Services.Email;
using MediatR;

namespace BloodBank.Application.Events;

public class LowStockEventHandler(
    IUserRepository userRepository,
    IEmailService emailService) : INotificationHandler<DomainEventNotification<LowStockDomainEvent>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmailService _emailService = emailService;

    public async Task Handle(DomainEventNotification<LowStockDomainEvent> notification, CancellationToken cancellationToken)
    {
        var usersToNotify = await _userRepository.GetUsersToNotifyLowStockAsync();

        if (!usersToNotify.Any())
            return;

        var emails = usersToNotify.Select(user => user.Email).ToList();
        var subject = "Alerta de Baixo Estoque de Sangue";
        var body =
            $"O estoque para {notification.DomainEvent.BloodType}{notification.DomainEvent.RhFactor.ToDisplayString()} está baixo. " +
            $"Quantidade atual: {notification.DomainEvent.QuantityML}ml.";

        await _emailService.SendAsync(emails, subject, body);
    }
}
