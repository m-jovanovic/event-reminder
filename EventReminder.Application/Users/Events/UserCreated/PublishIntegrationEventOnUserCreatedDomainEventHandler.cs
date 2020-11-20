using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events;

namespace EventReminder.Application.Users.Events.UserCreated
{
    /// <summary>
    /// Represents the <see cref="UserCreatedDomainEvent"/> handler.
    /// </summary>
    internal sealed class PublishIntegrationEventOnUserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishIntegrationEventOnUserCreatedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public PublishIntegrationEventOnUserCreatedDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher) =>
            _integrationEventPublisher = integrationEventPublisher;

        /// <inheritdoc />
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new UserCreatedIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
