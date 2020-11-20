using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Common;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events;
using EventReminder.Domain.Repositories;

namespace EventReminder.Application.GroupEvents.Events.GroupEventCancelled
{
    /// <summary>
    /// Represents the <see cref="GroupEventCancelledDomainEvent"/> class.
    /// </summary>
    internal sealed class RemoveInvitationsOnGroupEventCancelledDomainEventHandler : IDomainEventHandler<GroupEventCancelledDomainEvent>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveInvitationsOnGroupEventCancelledDomainEventHandler"/> class.
        /// </summary>
        /// <param name="invitationRepository">The invitation repository.</param>
        /// <param name="dateTime">The date and time.</param>
        public RemoveInvitationsOnGroupEventCancelledDomainEventHandler(IInvitationRepository invitationRepository, IDateTime dateTime)
        {
            _invitationRepository = invitationRepository;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task Handle(GroupEventCancelledDomainEvent notification, CancellationToken cancellationToken) =>
            await _invitationRepository.RemoveInvitationsForGroupEventAsync(notification.GroupEvent, _dateTime.UtcNow);
    }
}
