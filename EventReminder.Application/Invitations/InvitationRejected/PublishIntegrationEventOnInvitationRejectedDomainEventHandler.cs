using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Invitations.DomainEvents;

namespace EventReminder.Application.Invitations.InvitationRejected
{
    /// <summary>
    /// Represents the <see cref="InvitationSentDomainEvent"/> handler.
    /// </summary>
    internal sealed class PublishIntegrationEventOnInvitationRejectedDomainEventHandler
        : IDomainEventHandler<InvitationRejectedDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishIntegrationEventOnInvitationRejectedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public PublishIntegrationEventOnInvitationRejectedDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher) =>
            _integrationEventPublisher = integrationEventPublisher;

        /// <inheritdoc />
        public async Task Handle(InvitationRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new InvitationRejectedIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
