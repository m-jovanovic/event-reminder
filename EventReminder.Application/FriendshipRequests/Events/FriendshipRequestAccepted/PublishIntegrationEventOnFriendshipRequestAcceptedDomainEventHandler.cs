using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events;

namespace EventReminder.Application.FriendshipRequests.Events.FriendshipRequestAccepted
{
    /// <summary>
    /// Represents the <see cref="FriendshipRequestSentDomainEvent"/> handler.
    /// </summary>
    internal sealed class PublishIntegrationEventOnFriendshipRequestAcceptedDomainEventHandler
        : IDomainEventHandler<FriendshipRequestAcceptedDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishIntegrationEventOnFriendshipRequestAcceptedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public PublishIntegrationEventOnFriendshipRequestAcceptedDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher)
            => _integrationEventPublisher = integrationEventPublisher;

        /// <inheritdoc />
        public async Task Handle(FriendshipRequestAcceptedDomainEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new FriendshipRequestAcceptedIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
