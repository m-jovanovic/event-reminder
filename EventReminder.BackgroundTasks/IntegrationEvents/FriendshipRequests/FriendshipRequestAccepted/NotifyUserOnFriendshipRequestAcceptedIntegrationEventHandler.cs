using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Notifications;
using EventReminder.Application.FriendshipRequests.Events.FriendshipRequestAccepted;
using EventReminder.BackgroundTasks.Abstractions.Messaging;
using EventReminder.Contracts.Emails;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Exceptions;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Repositories;

namespace EventReminder.BackgroundTasks.IntegrationEvents.FriendshipRequests.FriendshipRequestAccepted
{
    /// <summary>
    /// Represents the <see cref="FriendshipRequestAcceptedIntegrationEvent"/> handler.
    /// </summary>
    internal sealed class NotifyUserOnFriendshipRequestAcceptedIntegrationEventHandler
        : IIntegrationEventHandler<FriendshipRequestAcceptedIntegrationEvent>
    {
        private readonly IFriendshipRequestRepository _friendshipRequestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyUserOnFriendshipRequestAcceptedIntegrationEventHandler"/> class.
        /// </summary>
        /// <param name="friendshipRequestRepository">The friendship request repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="emailNotificationService">The email notification service.</param>
        public NotifyUserOnFriendshipRequestAcceptedIntegrationEventHandler(
            IFriendshipRequestRepository friendshipRequestRepository,
            IUserRepository userRepository,
            IEmailNotificationService emailNotificationService)
        {
            _friendshipRequestRepository = friendshipRequestRepository;
            _userRepository = userRepository;
            _emailNotificationService = emailNotificationService;
        }

        /// <inheritdoc />
        public async Task Handle(FriendshipRequestAcceptedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            Maybe<FriendshipRequest> maybeFriendshipRequest = await _friendshipRequestRepository
                .GetByIdAsync(notification.FriendshipRequestId);

            if (maybeFriendshipRequest.HasNoValue)
            {
                throw new DomainException(DomainErrors.FriendshipRequest.NotFound);
            }

            FriendshipRequest friendshipRequest = maybeFriendshipRequest.Value;

            Maybe<User> maybeUser = await _userRepository.GetByIdAsync(friendshipRequest.UserId);

            if (maybeUser.HasNoValue)
            {
                throw new DomainException(DomainErrors.FriendshipRequest.UserNotFound);
            }

            Maybe<User> maybeFriend = await _userRepository.GetByIdAsync(friendshipRequest.FriendId);

            if (maybeFriend.HasNoValue)
            {
                throw new DomainException(DomainErrors.FriendshipRequest.FriendNotFound);
            }

            User user = maybeUser.Value;

            User friend = maybeFriend.Value;

            var friendshipRequestAcceptedEmail = new FriendshipRequestAcceptedEmail(user.Email, user.FullName, friend.FullName);

            await _emailNotificationService.SendFriendshipRequestAcceptedEmail(friendshipRequestAcceptedEmail);
        }
    }
}
