using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Messaging;

namespace EventReminder.Application.Attendees.AttendeeCreated
{
    /// <summary>
    /// Represents the <see cref="AttendeeCreatedEvent"/> handler.
    /// </summary>
    internal sealed class PublishIntegrationEventOnAttendeeCreatedEventHandler : IEventHandler<AttendeeCreatedEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishIntegrationEventOnAttendeeCreatedEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public PublishIntegrationEventOnAttendeeCreatedEventHandler(IIntegrationEventPublisher integrationEventPublisher) =>
            _integrationEventPublisher = integrationEventPublisher;

        /// <inheritdoc />
        public async Task Handle(AttendeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new AttendeeCreatedIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
