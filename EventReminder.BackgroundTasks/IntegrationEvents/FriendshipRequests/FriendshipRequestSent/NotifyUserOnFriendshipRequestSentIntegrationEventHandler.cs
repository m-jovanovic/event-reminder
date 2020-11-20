using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Notifications;
using EventReminder.Application.FriendshipRequests.Events.FriendshipRequestSent;
using EventReminder.BackgroundTasks.Abstractions.Messaging;
using EventReminder.Contracts.Emails;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Exceptions;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Repositories;

namespace EventReminder.BackgroundTasks.IntegrationEvents.FriendshipRequests.FriendshipRequestSent
{
    /// <summary>
    /// Represents the <see cref="FriendshipRequestSentIntegrationEvent"/> handler.
    /// </summary>
    internal sealed class NotifyUserOnFriendshipRequestSentIntegrationEventHandler
        : IIntegrationEventHandler<FriendshipRequestSentIntegrationEvent>
    {
        private readonly IFriendshipRequestRepository _friendshipRequestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyUserOnFriendshipRequestSentIntegrationEventHandler"/> class.
        /// </summary>
        /// <param name="friendshipRequestRepository">The friendship request repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="emailNotificationService">The email notification service.</param>
        public NotifyUserOnFriendshipRequestSentIntegrationEventHandler(
            IFriendshipRequestRepository friendshipRequestRepository,
            IUserRepository userRepository,
            IEmailNotificationService emailNotificationService)
        {
            _friendshipRequestRepository = friendshipRequestRepository;
            _userRepository = userRepository;
            _emailNotificationService = emailNotificationService;
        }

        /// <inheritdoc />
        public async Task Handle(FriendshipRequestSentIntegrationEvent notification, CancellationToken cancellationToken)
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

            var friendshipRequestSentEmail = new FriendshipRequestSentEmail(friend.Email, friend.FullName, user.FullName);

            await _emailNotificationService.SendFriendshipRequestSentEmail(friendshipRequestSentEmail);
        }
    }
}
