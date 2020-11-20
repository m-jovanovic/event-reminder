using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Events
{
    /// <summary>
    /// Represents the event that is raised when a friendship request is rejected.
    /// </summary>
    public sealed class FriendshipRequestRejectedDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRequestRejectedDomainEvent"/> class.
        /// </summary>
        /// <param name="friendshipRequest">The friendship request.</param>
        internal FriendshipRequestRejectedDomainEvent(FriendshipRequest friendshipRequest) => FriendshipRequest = friendshipRequest;

        /// <summary>
        /// Gets the friendship request.
        /// </summary>
        public FriendshipRequest FriendshipRequest { get; }
    }
}