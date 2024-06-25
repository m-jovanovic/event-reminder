using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Abstractions.Notifications;
using EventReminder.Application.Users.ChangePassword;
using EventReminder.BackgroundTasks.Abstractions.Messaging;
using EventReminder.Contracts.Emails;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Exceptions;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Users;

namespace EventReminder.BackgroundTasks.IntegrationEvents.Users.UserPasswordChanged
{
    /// <summary>
    /// Represents the <see cref="UserPasswordChangedIntegrationEvent"/> handler.
    /// </summary>
    internal sealed class NotifyUserOnPasswordChangedIntegrationEventHandler
        : IIntegrationEventHandler<UserPasswordChangedIntegrationEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyUserOnPasswordChangedIntegrationEventHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="emailNotificationService">The email notification service.</param>
        public NotifyUserOnPasswordChangedIntegrationEventHandler(
            IUserRepository userRepository,
            IEmailNotificationService emailNotificationService)
        {
            _emailNotificationService = emailNotificationService;
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task Handle(UserPasswordChangedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            Maybe<User> maybeUser = await _userRepository.GetByIdAsync(notification.UserId);

            if (maybeUser.HasNoValue)
            {
                throw new DomainException(DomainErrors.User.NotFound);
            }

            User user = maybeUser.Value;

            var passwordChangedEmail = new PasswordChangedEmail(user.Email, user.FullName);

            await _emailNotificationService.SendPasswordChangedEmail(passwordChangedEmail);
        }
    }
}
