using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Notifications;
using EventReminder.Application.Invitations.Events.InvitationSent;
using EventReminder.BackgroundTasks.Abstractions.Messaging;
using EventReminder.Contracts.Emails;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Exceptions;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Repositories;

namespace EventReminder.BackgroundTasks.IntegrationEvents.Invitations.InvitationSent
{
    /// <summary>
    /// Represents the <see cref="InvitationSentIntegrationEvent"/> handler.
    /// </summary>
    internal sealed class NotifyFriendOnInvitationSentIntegrationEventHandler : IIntegrationEventHandler<InvitationSentIntegrationEvent>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IGroupEventRepository _groupEventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyFriendOnInvitationSentIntegrationEventHandler"/> class.
        /// </summary>
        /// <param name="invitationRepository">The invitation repository.</param>
        /// <param name="groupEventRepository">The group event repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="emailNotificationService">The email notification service.</param>
        public NotifyFriendOnInvitationSentIntegrationEventHandler(
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
        public async Task Handle(InvitationSentIntegrationEvent notification, CancellationToken cancellationToken)
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
            
            Maybe<User> maybeUser = await _userRepository.GetByIdAsync(invitation.UserId);

            if (maybeUser.HasNoValue)
            {
                throw new DomainException(DomainErrors.Invitation.FriendNotFound);
            }

            User user = maybeUser.Value;

            var invitationSentEmail = new InvitationSentEmail(
                user.Email,
                user.FullName,
                groupEvent.Name,
                groupEvent.DateTimeUtc.ToString(CultureInfo.InvariantCulture));

            await _emailNotificationService.SendInvitationSentEmail(invitationSentEmail);
        }
    }
}
