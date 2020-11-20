using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Common;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Events;
using EventReminder.Domain.Repositories;

namespace EventReminder.Application.Friendships.Events.FriendshipRemoved
{
    /// <summary>
    /// Represents the <see cref="FriendshipRemovedDomainEvent"/> handler.
    /// </summary>
    internal sealed class RemoveInvitationsOnFriendshipRemovedDomainEventHandler : IDomainEventHandler<FriendshipRemovedDomainEvent>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveInvitationsOnFriendshipRemovedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="invitationRepository">The invitation repository.</param>
        /// <param name="dateTime">The date and time.</param>
        public RemoveInvitationsOnFriendshipRemovedDomainEventHandler(IInvitationRepository invitationRepository, IDateTime dateTime)
        {
            _invitationRepository = invitationRepository;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task Handle(FriendshipRemovedDomainEvent notification, CancellationToken cancellationToken) =>
            await _invitationRepository.RemovePendingInvitationsForFriendshipAsync(notification.Friendship, _dateTime.UtcNow);
    }
}
