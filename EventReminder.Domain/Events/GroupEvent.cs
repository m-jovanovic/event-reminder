using System;
using System.Threading.Tasks;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives.Result;
using EventReminder.Domain.Events.DomainEvents;
using EventReminder.Domain.Invitations;
using EventReminder.Domain.Invitations.DomainEvents;
using EventReminder.Domain.Users;

namespace EventReminder.Domain.Events
{
    /// <summary>
    /// Represents a group event.
    /// </summary>
    public sealed class GroupEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEvent"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="name">The event name.</param>
        /// <param name="category">The category.</param>
        /// <param name="dateTimeUtc">The date and time of the event in UTC format.</param>
        private GroupEvent(User user, Name name, Category category, DateTime dateTimeUtc)
            : base(user, name, category, dateTimeUtc, EventType.GroupEvent)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEvent"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private GroupEvent()
        {
        }

        /// <summary>
        /// Creates a new group event based on the specified parameters.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="dateTimeUtc">The date and time in UTC format.</param>
        /// <returns>The newly created group event.</returns>
        public static GroupEvent Create(User user, Name name, Category category, DateTime dateTimeUtc)
        {
            var groupEvent = new GroupEvent(user, name, category, dateTimeUtc);

            groupEvent.AddDomainEvent(new GroupEventCreatedDomainEvent(groupEvent));

            return groupEvent;
        }

        /// <summary>
        /// Invites the specified user to the event.
        /// </summary>
        /// <param name="user">The user to be invited.</param>
        /// <param name="invitationRepository">The invitation repository.</param>
        /// <returns>The result that contains an invitation or an error.</returns>
        public async Task<Result<Invitation>> InviteAsync(User user, IInvitationRepository invitationRepository)
        {
            if (await invitationRepository.CheckIfInvitationAlreadySentAsync(this, user))
            {
                return Result.Failure<Invitation>(DomainErrors.GroupEvent.InvitationAlreadySent);
            }

            var invitation = new Invitation(this, user);

            AddDomainEvent(new InvitationSentDomainEvent(invitation));

            return invitation;
        }

        /// <summary>
        /// Gets the event owner.
        /// </summary>
        /// <returns>The event owner attendee instance.</returns>
        public Attendee GetOwner() => new Attendee(this);

        /// <inheritdoc />
        public override Result Cancel(DateTime utcNow)
        {
            Result result = base.Cancel(utcNow);

            if (result.IsSuccess)
            {
                AddDomainEvent(new GroupEventCancelledDomainEvent(this));
            }

            return result;
        }

        /// <inheritdoc />
        public override bool ChangeName(Name name)
        {
            string previousName = Name;

            bool hasChanged = base.ChangeName(name);

            if (hasChanged)
            {
                AddDomainEvent(new GroupEventNameChangedDomainEvent(this, previousName));
            }

            return hasChanged;
        }

        /// <inheritdoc />
        public override bool ChangeDateAndTime(DateTime dateTimeUtc)
        {
            DateTime previousDateAndTime = DateTimeUtc;

            bool hasChanged = base.ChangeDateAndTime(dateTimeUtc);

            if (hasChanged)
            {
                AddDomainEvent(new GroupEventDateAndTimeChangedDomainEvent(this, previousDateAndTime));
            }

            return hasChanged;
        }
    }
}