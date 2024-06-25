using System;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.PersonalEvents.UpdatePersonalEvent
{
    /// <summary>
    /// Represents the update personal event command.
    /// </summary>
    public sealed class UpdatePersonalEventCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePersonalEventCommand"/> class.
        /// </summary>
        /// <param name="personalEventId">The personal event identifier.</param>
        /// <param name="name">The event name.</param>
        /// <param name="dateTimeUtc">The date and time of the event in UTC format.</param>
        public UpdatePersonalEventCommand(Guid personalEventId, string name, DateTime dateTimeUtc)
        {
            PersonalEventId = personalEventId;
            Name = name;
            DateTimeUtc = dateTimeUtc.ToUniversalTime();
        }

        /// <summary>
        /// Gets the personal event identifier.
        /// </summary>
        public Guid PersonalEventId { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the date and time in UTC format.
        /// </summary>
        public DateTime DateTimeUtc { get; }
    }
}
