using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Events
{
    /// <summary>
    /// Represents the event that is raised when a friendship request is accepted.
    /// </summary>
    public sealed class FriendshipRequestAcceptedDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRequestAcceptedDomainEvent"/> class.
        /// </summary>
        /// <param name="friendshipRequest">The friendship request.</param>
        internal FriendshipRequestAcceptedDomainEvent(FriendshipRequest friendshipRequest) => FriendshipRequest = friendshipRequest;

        /// <summary>
        /// Gets the friendship request.
        /// </summary>
        public FriendshipRequest FriendshipRequest { get; }
    }
}
