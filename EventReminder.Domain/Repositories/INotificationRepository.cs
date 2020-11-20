using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Repositories
{
    /// <summary>
    /// Represents the notifications repository.
    /// </summary>
    public interface INotificationRepository
    {
        /// <summary>
        /// Gets the notifications along with their respective events and users that are waiting to be sent.
        /// </summary>
        /// <param name="batchSize">The batch size.</param>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <param name="allowedNotificationTimeDiscrepancyInMinutes">
        /// The allowed discrepancy between the current time and the time the notification is supposed to be sent.
        /// </param>
        /// <returns>The notifications along with their respective events and users that are waiting to be sent.</returns>
        Task<(Notification Notification, Event Event, User User)[]> GetNotificationsToBeSentIncludingUserAndEvent(
            int batchSize,
            DateTime utcNow,
            int allowedNotificationTimeDiscrepancyInMinutes);

        /// <summary>
        /// Inserts the specified notifications to the database.
        /// </summary>
        /// <param name="notifications">The notifications to be inserted into the database.</param>
        void InsertRange(IReadOnlyCollection<Notification> notifications);

        /// <summary>
        /// Updates the specified notification in the database.
        /// </summary>
        /// <param name="notification">The notification to be updated.</param>
        void Update(Notification notification);

        /// <summary>
        /// Removes all of the notifications for the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The completed task.</returns>
        Task RemoveNotificationsForEventAsync(Event @event, DateTime utcNow);
    }
}