using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.GroupEvents.Commands.CancelGroupEvent
{
    /// <summary>
    /// Represents the cancel group event command.
    /// </summary>
    public sealed class CancelGroupEventCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelGroupEventCommand"/> class.
        /// </summary>
        /// <param name="groupEventId">The group event identifier.</param>
        public CancelGroupEventCommand(Guid groupEventId) => GroupEventId = groupEventId;

        /// <summary>
        /// Gets the group event identifier.
        /// </summary>
        public Guid GroupEventId { get; }
    }
}
