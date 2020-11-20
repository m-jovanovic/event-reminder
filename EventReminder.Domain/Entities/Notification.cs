using System;
using EventReminder.Domain.Core.Abstractions;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Core.Utility;
using EventReminder.Domain.Enumerations;

namespace EventReminder.Domain.Entities
{
    /// <summary>
    /// Represents the notification.
    /// </summary>
    public sealed class Notification : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notificationType">The notification type.</param>
        /// <param name="dateTimeUtc">The date and time of the notification.</param>
        internal Notification(Guid eventId, Guid userId, NotificationType notificationType, DateTime dateTimeUtc)
            : base(Guid.NewGuid())
        {
            Ensure.NotEmpty(eventId, "The event identifier is required.", nameof(eventId));
            Ensure.NotEmpty(userId, "The user identifier is required.", nameof(userId));
            Ensure.NotEmpty(dateTimeUtc, "The date and time is required.", nameof(dateTimeUtc));

            EventId = eventId;
            UserId = userId;
            NotificationType = notificationType;
            DateTimeUtc = dateTimeUtc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Notification()
        {
        }
        
        /// <summary>
        /// Gets the event identifier.
        /// </summary>
        public Guid EventId { get; private set; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Gets the notification type.
        /// </summary>
        public NotificationType NotificationType { get; private set; }

        /// <summary>
        /// Gets the date and time in UTC format of when the notification is supposed to be sent.
        /// </summary>
        public DateTime DateTimeUtc { get; private set; }

        /// <summary>
        /// Gets the value indicating whether or not the notification has been sent.
        /// </summary>
        public bool Sent { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }

        /// <summary>
        /// Marks the notification as sent and returns the respective result.
        /// </summary>
        /// <returns>The success result if the notification was not previously marked as sent, otherwise a failure result.</returns>
        public Result MarkAsSent()
        {
            if (Sent)
            {
                return Result.Failure(DomainErrors.Notification.AlreadySent);
            }

            Sent = true;

            return Result.Success();
        }

        /// <summary>
        /// Creates the notification email for the specified event and user.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <param name="user">The user.</param>
        /// <returns>The email subject and body.</returns>
        public (string, string) CreateNotificationEmail(Event @event, User user)
        {
            if (@event.Id != EventId)
            {
                throw new InvalidOperationException("The specified event is not valid for this notification.");
            }

            if (user.Id != UserId)
            {
                throw new InvalidOperationException("The specified user is not valid for this notification.");
            }

            return NotificationType.CreateNotificationEmail(@event, user);
        }
    }
}
