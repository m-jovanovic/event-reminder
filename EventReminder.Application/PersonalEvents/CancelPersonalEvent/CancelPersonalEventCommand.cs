using System;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.PersonalEvents.CancelPersonalEvent
{
    /// <summary>
    /// Represents the cancel personal event command.
    /// </summary>
    public sealed class CancelPersonalEventCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelPersonalEventCommand"/> class.
        /// </summary>
        /// <param name="personalEventId">The personal event identifier.</param>
        public CancelPersonalEventCommand(Guid personalEventId) => PersonalEventId = personalEventId;

        /// <summary>
        /// Gets the personal event identifier.
        /// </summary>
        public Guid PersonalEventId { get; }
    }
}
