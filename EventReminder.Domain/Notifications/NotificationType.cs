using System;
using EventReminder.Domain.Core.Primitives;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Events;
using EventReminder.Domain.Users;

namespace EventReminder.Domain.Notifications
{
    /// <summary>
    /// Represents the notification types enumeration.
    /// </summary>
    public abstract class NotificationType : Enumeration<NotificationType>
    {
        /// <summary>
        /// The day before notification type.
        /// </summary>
        public static readonly NotificationType DayBefore = new DayBeforeNotificationType();

        /// <summary>
        /// The hour before notification type.
        /// </summary>
        public static readonly NotificationType HourBefore = new HourBeforeNotificationType();

        /// <summary>
        /// The fifteen minutes before notification type.
        /// </summary>
        public static readonly NotificationType FifteenMinutesBefore = new FifteenMinutesBeforeNotificationType();

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationType"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="name">The name.</param>
        private NotificationType(int value, string name)
            : base(value, name)
        {
        }

        /// <summary>
        /// Attempts to create a notification for the specified group event and user identifier, based on the current date and time.
        /// </summary>
        /// <param name="event">The group event.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="utcNow">The current date and time.</param>
        /// <returns>The maybe instance that may contain the notification.</returns>
        public abstract Maybe<Notification> TryCreateNotification(Event @event, Guid userId, DateTime utcNow);

        /// <summary>
        /// Creates the notification email for the specified event and user.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <param name="user">The user.</param>
        /// <returns>The email subject and body.</returns>
        internal abstract (string subject, string body) CreateNotificationEmail(Event @event, User user);

        /// <summary>
        /// Represents the day before notification type.
        /// </summary>
        private class DayBeforeNotificationType : NotificationType
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DayBeforeNotificationType"/> class.
            /// </summary>
            public DayBeforeNotificationType()
                : base(1, "Day before")
            {
            }

            /// <inheritdoc />
            public override Maybe<Notification> TryCreateNotification(Event @event, Guid userId, DateTime utcNow)
            {
                const int allowedDifferenceInDays = 1;

                if ((@event.DateTimeUtc - utcNow).Days < allowedDifferenceInDays)
                {
                    return Maybe<Notification>.None;
                }

                return new Notification(@event.Id, userId, this, @event.DateTimeUtc.AddDays(-allowedDifferenceInDays));
            }

            /// <inheritdoc />
            internal override (string subject, string body) CreateNotificationEmail(Event @event, User user) =>
                ($"You have {@event.Name} in one day!! 🎉",
                    $"Hello {user.FullName}," +
                    Environment.NewLine +
                    Environment.NewLine +
                    $"The event you are attending {@event.Name} will be taking place in one day.");
        }

        /// <summary>
        /// Represents the hour before notification type.
        /// </summary>
        private class HourBeforeNotificationType : NotificationType
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="HourBeforeNotificationType"/> class.
            /// </summary>
            public HourBeforeNotificationType()
                : base(2, "Hour before")
            {
            }

            /// <inheritdoc />
            public override Maybe<Notification> TryCreateNotification(Event @event, Guid userId, DateTime utcNow)
            {
                const int allowedDifferenceInHours = 1;

                if ((@event.DateTimeUtc - utcNow).Hours < allowedDifferenceInHours)
                {
                    return Maybe<Notification>.None;
                }

                return new Notification(@event.Id, userId, this, @event.DateTimeUtc.AddHours(-allowedDifferenceInHours));
            }

            /// <inheritdoc />
            internal override (string subject, string body) CreateNotificationEmail(Event @event, User user) =>
                ($"You have {@event.Name} in 1 hour! 🎉",
                    $"Hello {user.FullName}," +
                    Environment.NewLine +
                    Environment.NewLine +
                    $"The event you are attending {@event.Name} will be taking place in one hour from now.");
        }

        /// <summary>
        /// Represents the fifteen minutes before notification type.
        /// </summary>
        private class FifteenMinutesBeforeNotificationType : NotificationType
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="FifteenMinutesBeforeNotificationType"/> class.
            /// </summary>
            public FifteenMinutesBeforeNotificationType()
                : base(3, "Fifteen minutes before")
            {
            }

            /// <inheritdoc />
            public override Maybe<Notification> TryCreateNotification(Event @event, Guid userId, DateTime utcNow)
            {
                const int allowedDifferenceInMinutes = 15;

                if ((@event.DateTimeUtc - utcNow).Minutes < allowedDifferenceInMinutes)
                {
                    return Maybe<Notification>.None;
                }

                return new Notification(@event.Id, userId, this, @event.DateTimeUtc.AddMinutes(-allowedDifferenceInMinutes));
            }

            /// <inheritdoc />
            internal override (string subject, string body) CreateNotificationEmail(Event @event, User user) =>
                ($"You have {@event.Name} in 15 minutes! 🎉",
                    $"Hello {user.FullName}," +
                    Environment.NewLine +
                    Environment.NewLine +
                    $"The event you are attending {@event.Name} will be taking place 15 minutes from now.");
        }
    }
}
