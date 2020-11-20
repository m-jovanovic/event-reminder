using System;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Enumerations;
using EventReminder.Domain.Events;
using EventReminder.Domain.ValueObjects;

namespace EventReminder.Domain.Entities
{
    /// <summary>
    /// Represents a personal event.
    /// </summary>
    public sealed class PersonalEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEvent"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="name">The event name.</param>
        /// <param name="category">The category.</param>
        /// <param name="dateTimeUtc">The date and time of the event in UTC format.</param>
        internal PersonalEvent(User user, Name name, Category category, DateTime dateTimeUtc)
            : base(user, name, category, dateTimeUtc, EventType.PersonalEvent)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEvent"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private PersonalEvent()
        {
        }

        /// <summary>
        /// Gets the value indicating whether or not the event has been processed.
        /// </summary>
        public bool Processed { get; private set; }

        /// <summary>
        /// Marks the event as processed and returns the respective result.
        /// </summary>
        /// <returns>The success result if the event was not previously marked as processed, otherwise a failure result.</returns>
        public Result MarkAsProcessed()
        {
            if (Processed)
            {
                return Result.Failure(DomainErrors.PersonalEvent.AlreadyProcessed);
            }

            Processed = true;

            return Result.Success();
        }

        /// <inheritdoc />
        public override Result Cancel(DateTime utcNow)
        {
            Result result = base.Cancel(utcNow);

            if (result.IsSuccess)
            {
                AddDomainEvent(new PersonalEventCancelledDomainEvent(this));
            }

            return result;
        }

        /// <inheritdoc />
        public override bool ChangeDateAndTime(DateTime dateTimeUtc)
        {
            bool hasChanged = base.ChangeDateAndTime(dateTimeUtc);

            if (hasChanged)
            {
                AddDomainEvent(new PersonalEventDateAndTimeChangedDomainEvent(this));

                Processed = false;
            }

            return hasChanged;
        }
    }
}
