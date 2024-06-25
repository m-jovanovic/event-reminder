using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Users.DomainEvents;
using System.Threading;
using System.Threading.Tasks;

namespace EventReminder.Application.Users.CreateUser
{
    /// <summary>
    /// Represents the <see cref="UserCreatedDomainEvent"/> handler.
    /// </summary>
    internal sealed class PublishIntegrationEventOnUserCreatedDomainEventHandler
        : IDomainEventHandler<UserCreatedDomainEvent>
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
