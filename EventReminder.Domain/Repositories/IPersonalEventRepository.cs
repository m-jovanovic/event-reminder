using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;

namespace EventReminder.Domain.Repositories
{
    /// <summary>
    /// Represents the personal event repository interface.
    /// </summary>
    public interface IPersonalEventRepository
    {
        /// <summary>
        /// Gets the personal event with the specified identifier.
        /// </summary>
        /// <param name="personalEventId">The personal event identifier.</param>
        /// <returns>The maybe instance that may contain the personal event with the specified identifier.</returns>
        Task<Maybe<PersonalEvent>> GetByIdAsync(Guid personalEventId);

        /// <summary>
        /// Gets the specified number of unprocessed personal events, if they exist.
        /// </summary>
        /// <param name="take">The number of personal events to take.</param>
        /// <returns>The specified number of unprocessed personal events, if they exist.</returns>
        Task<IReadOnlyCollection<PersonalEvent>> GetUnprocessedAsync(int take);

        /// <summary>
        /// Inserts the specified personal event to the database.
        /// </summary>
        /// <param name="personalEvent">The personal event to be inserted to the database.</param>
        void Insert(PersonalEvent personalEvent);
    }
}
