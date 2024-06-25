using System;
using EventReminder.Domain.Core.Abstractions;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Guards;
using EventReminder.Domain.Core.Primitives;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Invitations;

namespace EventReminder.Domain.Events
{
    /// <summary>
    /// Represents an attendee to a group event.
    /// </summary>
    public sealed class Attendee : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Attendee"/> class.
        /// </summary>
        /// <param name="invitation">The invitation.</param>
        public Attendee(Invitation invitation)
            : base(Guid.NewGuid())
        {
            Ensure.NotNull(invitation, "The invitation is required.", nameof(invitation));
            Ensure.NotEmpty(invitation.EventId, "The event identifier is required.", nameof(invitation.EventId));
            Ensure.NotEmpty(invitation.UserId, "The user identifier is required.", nameof(invitation.UserId));

            EventId = invitation.EventId;
            UserId = invitation.UserId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attendee"/> class.
        /// </summary>
        /// <param name="groupEvent">The group event.</param>
        internal Attendee(GroupEvent groupEvent)
        {
            Ensure.NotNull(groupEvent, "The group event is required.", nameof(groupEvent));
            Ensure.NotEmpty(groupEvent.Id, "The event identifier is required.", nameof(groupEvent.Id));
            Ensure.NotEmpty(groupEvent.UserId, "The user identifier is required.", nameof(groupEvent.UserId));

            EventId = groupEvent.Id;
            UserId = groupEvent.UserId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attendee"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Attendee()
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
        /// Gets the value indicating whether or not the event has been processed.
        /// </summary>
        public bool Processed { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }

        /// <summary>
        /// Marks the attendee as processed and returns the respective result.
        /// </summary>
        /// <returns>The success result if the attendee was not previously marked as processed, otherwise a failure result.</returns>
        public Result MarkAsProcessed()
        {
            if (Processed)
            {
                return Result.Failure(DomainErrors.Attendee.AlreadyProcessed);
            }

            Processed = true;

            return Result.Success();
        }
    }
}
