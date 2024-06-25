using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Notifications;
using EventReminder.Application.Invitations.InvitationRejected;
using EventReminder.BackgroundTasks.Abstractions.Messaging;
using EventReminder.Contracts.Emails;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Exceptions;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Events;
using EventReminder.Domain.Invitations;
using EventReminder.Domain.Users;

namespace EventReminder.BackgroundTasks.IntegrationEvents.Invitations.InvitationRejected
{
    /// <summary>
    /// Represents the <see cref="InvitationRejectedIntegrationEvent"/> handler.
    /// </summary>
    internal sealed class NotifyEventOwnerOnInvitationRejectedIntegrationEventHandler
        : IIntegrationEventHandler<InvitationRejectedIntegrationEvent>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IGroupEventRepository _groupEventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyEventOwnerOnInvitationRejectedIntegrationEventHandler"/> class.
        /// </summary>
        /// <param name="invitationRepository">The invitation repository.</param>
        /// <param name="groupEventRepository">The group event repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="emailNotificationService">The email notification service.</param>
        public NotifyEventOwnerOnInvitationRejectedIntegrationEventHandler(
            IInvitationRepository invitationRepository,
            IGroupEventRepository groupEventRepository,
            IUserRepository userRepository,
            IEmailNotificationService emailNotificationService)
        {
            _invitationRepository = invitationRepository;
            _groupEventRepository = groupEventRepository;
            _userRepository = userRepository;
            _emailNotificationService = emailNotificationService;
        }

        /// <inheritdoc />
        public async Task Handle(InvitationRejectedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            Maybe<Invitation> maybeInvitation = await _invitationRepository.GetByIdAsync(notification.InvitationId);

            if (maybeInvitation.HasNoValue)
            {
                throw new DomainException(DomainErrors.Invitation.NotFound);
            }

            Invitation invitation = maybeInvitation.Value;

            Maybe<GroupEvent> maybeGroupEvent = await _groupEventRepository.GetByIdAsync(invitation.EventId);

            if (maybeGroupEvent.HasNoValue)
            {
                throw new DomainException(DomainErrors.Invitation.EventNotFound);
            }

            GroupEvent groupEvent = maybeGroupEvent.Value;

            Maybe<User> maybeUser = await _userRepository.GetByIdAsync(groupEvent.UserId);

            if (maybeUser.HasNoValue)
            {
                throw new DomainException(DomainErrors.Invitation.UserNotFound);
            }

            Maybe<User> maybeFriend = await _userRepository.GetByIdAsync(invitation.UserId);

            if (maybeFriend.HasNoValue)
            {
                throw new DomainException(DomainErrors.Invitation.FriendNotFound);
            }

            User user = maybeUser.Value;

            User friend = maybeFriend.Value;

            var invitationRejectedEmail = new InvitationRejectedEmail(
                user.Email,
                user.FullName,
                friend.FullName,
                groupEvent.Name,
                groupEvent.DateTimeUtc.ToString(CultureInfo.InvariantCulture) );

            await _emailNotificationService.SendInvitationRejectedEmail(invitationRejectedEmail);
        }
    }
}
