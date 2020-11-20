using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events;

namespace EventReminder.Application.FriendshipRequests.Events.FriendshipRequestSent
{
    /// <summary>
    /// Represents the <see cref="FriendshipRequestSentDomainEvent"/> handler.
    /// </summary>
    internal sealed class PublishIntegrationEventOnFriendshipRequestSentDomainEventHandler
        : IDomainEventHandler<FriendshipRequestSentDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishIntegrationEventOnFriendshipRequestSentDomainEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public PublishIntegrationEventOnFriendshipRequestSentDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher) =>
            _integrationEventPublisher = integrationEventPublisher;

        /// <inheritdoc />
        public async Task Handle(FriendshipRequestSentDomainEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new FriendshipRequestSentIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
