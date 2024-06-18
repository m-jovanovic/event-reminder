using System;
using System.Text.Json.Serialization;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Events;

namespace EventReminder.Application.Users.Events.PasswordChanged
{
    /// <summary>
    /// Represents the integration event that is raised when a user's password is changed.
    /// </summary>
    public sealed class UserPasswordChangedIntegrationEvent : IIntegrationEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPasswordChangedIntegrationEvent"/> class.
        /// </summary>
        /// <param name="userPasswordChangedDomainEvent">The user password changed domain event.</param>
        internal UserPasswordChangedIntegrationEvent(UserPasswordChangedDomainEvent userPasswordChangedDomainEvent) =>
            UserId = userPasswordChangedDomainEvent.User.Id;

        [JsonConstructor]
        private UserPasswordChangedIntegrationEvent(Guid userId) => UserId = userId;

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }
    }
}
