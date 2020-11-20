using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events;

namespace EventReminder.Application.Users.Events.PasswordChanged
{
    /// <summary>
    /// Represents the <see cref="UserPasswordChangedDomainEvent"/> handler.
    /// </summary>
    internal sealed class PublishIntegrationEventOnUserPasswordChangedDomainEventHandler
        : IDomainEventHandler<UserPasswordChangedDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishIntegrationEventOnUserPasswordChangedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public PublishIntegrationEventOnUserPasswordChangedDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher) =>
            _integrationEventPublisher = integrationEventPublisher;

        /// <inheritdoc />
        public async Task Handle(UserPasswordChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new UserPasswordChangedIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
