using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events.DomainEvents;

namespace EventReminder.Application.GroupEvents.GroupEventDateAndTimeChanged
{
    /// <summary>
    /// Represents the <see cref="GroupEventDateAndTimeChangedDomainEvent"/> class.
    /// </summary>
    internal sealed class PublishIntegrationEventOnGroupEventDateAndTimeChangedDomainEventHandler
        : IDomainEventHandler<GroupEventDateAndTimeChangedDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishIntegrationEventOnGroupEventDateAndTimeChangedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public PublishIntegrationEventOnGroupEventDateAndTimeChangedDomainEventHandler(
            IIntegrationEventPublisher integrationEventPublisher) =>
            _integrationEventPublisher = integrationEventPublisher;

        /// <inheritdoc />
        public async Task Handle(GroupEventDateAndTimeChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new GroupEventDateAndTimeChangedIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
