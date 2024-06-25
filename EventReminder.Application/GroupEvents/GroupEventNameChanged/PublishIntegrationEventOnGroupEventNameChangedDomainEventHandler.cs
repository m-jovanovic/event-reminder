using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events.DomainEvents;

namespace EventReminder.Application.GroupEvents.GroupEventNameChanged
{
    /// <summary>
    /// Represents the <see cref="GroupEventNameChangedDomainEvent"/> class.
    /// </summary>
    internal sealed class PublishIntegrationEventOnGroupEventNameChangedDomainEventHandler
        : IDomainEventHandler<GroupEventNameChangedDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishIntegrationEventOnGroupEventNameChangedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public PublishIntegrationEventOnGroupEventNameChangedDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher) =>
            _integrationEventPublisher = integrationEventPublisher;

        /// <inheritdoc />
        public async Task Handle(GroupEventNameChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new GroupEventNameChangedIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
