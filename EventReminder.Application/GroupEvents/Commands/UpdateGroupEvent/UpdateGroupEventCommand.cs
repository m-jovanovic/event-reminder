using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.GroupEvents.Commands.UpdateGroupEvent
{
    /// <summary>
    /// Represents the update group event command.
    /// </summary>
    public sealed class UpdateGroupEventCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGroupEventCommand"/> class.
        /// </summary>
        /// <param name="groupEventId">The group event identifier.</param>
        /// <param name="name">The event name.</param>
        /// <param name="dateTimeUtc">The date and time of the event in UTC format.</param>
        public UpdateGroupEventCommand(Guid groupEventId, string name, DateTime dateTimeUtc)
        {
            GroupEventId = groupEventId;
            Name = name;
            DateTimeUtc = dateTimeUtc.ToUniversalTime();
        }

        /// <summary>
        /// Gets the group event identifier.
        /// </summary>
        public Guid GroupEventId { get; }

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
