using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Invitations.DomainEvents;

namespace EventReminder.Application.Invitations.InvitationSent
{
    /// <summary>
    /// Represents the <see cref="InvitationRejectedDomainEvent"/> handler.
    /// </summary>
    internal sealed class PublishIntegrationEventOnInvitationSentDomainEventHandler : IDomainEventHandler<InvitationSentDomainEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishIntegrationEventOnInvitationSentDomainEventHandler"/> class.
        /// </summary>
        /// <param name="integrationEventPublisher">The integration event publisher.</param>
        public PublishIntegrationEventOnInvitationSentDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher) =>
            _integrationEventPublisher = integrationEventPublisher;

        /// <inheritdoc />
        public async Task Handle(InvitationSentDomainEvent notification, CancellationToken cancellationToken)
        {
            _integrationEventPublisher.Publish(new InvitationSentIntegrationEvent(notification));

            await Task.CompletedTask;
        }
    }
}
