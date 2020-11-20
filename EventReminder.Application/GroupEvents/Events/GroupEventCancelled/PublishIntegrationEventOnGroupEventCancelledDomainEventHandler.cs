using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events;

namespace EventReminder.Application.GroupEvents.Events.GroupEventCancelled
{
    /// <summary>
    /// Represents the <see cref="GroupEventCancelledDomainEvent"/> class.
    /// </summary>
    internal sealed class PublishIntegrationEventOnGroupEventCancelledDomainEventHandler
        : IDomainEventHandler<GroupEventCancelledDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishIntegrationEventOnGroupEventCancelledDomainEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public PublishIntegrationEventOnGroupEventCancelledDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher) =>
            _integrationEventPublisher = integrationEventPublisher;

        /// <inheritdoc />
        public async Task Handle(GroupEventCancelledDomainEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new GroupEventCancelledIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
