using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Repositories
{
    /// <summary>
    /// Represents the attendee repository interface.
    /// </summary>
    public interface IAttendeeRepository
    {
        /// <summary>
        /// Gets the attendee with the specified identifier.
        /// </summary>
        /// <param name="attendeeId">The attendee identifier.</param>
        /// <returns>The maybe instance that may contain the attendee with the specified identifier.</returns>
        Task<Maybe<Attendee>> GetByIdAsync(Guid attendeeId);

        /// <summary>
        /// Gets the specified number of unprocessed attendees, if they exist.
        /// </summary>
        /// <param name="take">The number of attendees to take.</param>
        /// <returns>The specified number of unprocessed attendees, if they exist.</returns>
        Task<IReadOnlyCollection<Attendee>> GetUnprocessedAsync(int take);

        /// <summary>
        /// Gets the emails and names of all of the attendees.
        /// </summary>
        /// <param name="groupEvent">The group event.</param>
        /// <returns>The emails and names of all of the attendees.</returns>
        Task<(string Email, string Name)[]> GetEmailsAndNamesForGroupEvent(GroupEvent groupEvent);

        /// <summary>
        /// Inserts the specified attendee to the database.
        /// </summary>
        /// <param name="attendee">The attendee to be inserted into the database.</param>
        void Insert(Attendee attendee);

        /// <summary>
        /// Marks the attendees as unprocessed for the specified group event.
        /// </summary>
        /// <param name="groupEvent">The group event.</param>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The completed task.</returns>
        Task MarkUnprocessedForGroupEventAsync(GroupEvent groupEvent, DateTime utcNow);

        /// <summary>
        /// Removes all of the attendees for the specified group event.
        /// </summary>
        /// <param name="groupEvent">The group event.</param>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The completed task.</returns>
        Task RemoveAttendeesForGroupEventAsync(GroupEvent groupEvent, DateTime utcNow);
    }
}
