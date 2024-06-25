using EventReminder.Domain.Core.Events;
using EventReminder.Domain.Friendships;

namespace EventReminder.Domain.Friendships.DomainEvents
{
    /// <summary>
    /// Represents the event that is raised when a friendship is removed.
    /// </summary>
    public sealed class FriendshipRemovedDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRemovedDomainEvent"/> class.
        /// </summary>
        /// <param name="friendship">The friendship.</param>
        internal FriendshipRemovedDomainEvent(Friendship friendship) => Friendship = friendship;

        /// <summary>
        /// Gets the friendship.
        /// </summary>
        public Friendship Friendship { get; }
    }
}
