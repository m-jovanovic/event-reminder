using System;

namespace EventReminder.Domain.Core.Abstractions
{
    /// <summary>
    /// Represents the marker interface for soft-deletable entities.
    /// </summary>
    public interface ISoftDeletableEntity
    {
        /// <summary>
        /// Gets the date and time in UTC format the entity was deleted on.
        /// </summary>
        DateTime? DeletedOnUtc { get; }

        /// <summary>
        /// Gets a value indicating whether the entity has been deleted.
        /// </summary>
        bool Deleted { get; }
    }
}
