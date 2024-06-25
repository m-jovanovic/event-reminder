using System;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Friendships.DomainEvents;
using Newtonsoft.Json;

namespace EventReminder.Application.FriendshipRequests.FriendshipRequestSent
{
    /// <summary>
    /// Represents the integration event that is raised when a friendship request is sent.
    /// </summary>
    public sealed class FriendshipRequestSentIntegrationEvent : IIntegrationEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRequestSentIntegrationEvent"/> class.
        /// </summary>
        /// <param name="friendshipRequestSentDomainEvent">The friendship request sent domain event.</param>
        internal FriendshipRequestSentIntegrationEvent(FriendshipRequestSentDomainEvent friendshipRequestSentDomainEvent) =>
            FriendshipRequestId = friendshipRequestSentDomainEvent.FriendshipRequest.Id;

        [JsonConstructor]
        private FriendshipRequestSentIntegrationEvent(Guid friendshipRequestId) => FriendshipRequestId = friendshipRequestId;

        /// <summary>
        /// Gets the friendship request identifier.
        /// </summary>
        public Guid FriendshipRequestId { get; }
    }
}
