using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Notifications;
using EventReminder.Application.Users.Events.UserCreated;
using EventReminder.BackgroundTasks.Abstractions.Messaging;
using EventReminder.Contracts.Emails;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Exceptions;
using EventReminder.Domain.Core.Primitives.Maybe;
using EventReminder.Domain.Entities;
using EventReminder.Domain.Repositories;

namespace EventReminder.BackgroundTasks.IntegrationEvents.Users.UserCreated
{
    /// <summary>
    /// Represents the <see cref="UserCreatedIntegrationEvent"/> handler.
    /// </summary>
    internal sealed class SendWelcomeEmailOnUserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendWelcomeEmailOnUserCreatedIntegrationEventHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="emailNotificationService">The email notification service.</param>
        public SendWelcomeEmailOnUserCreatedIntegrationEventHandler(
            IUserRepository userRepository,
            IEmailNotificationService emailNotificationService)
        {
            _emailNotificationService = emailNotificationService;
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            Maybe<User> maybeUser = await _userRepository.GetByIdAsync(notification.UserId);

            if (maybeUser.HasNoValue)
            {
                throw new DomainException(DomainErrors.User.NotFound);
            }

            User user = maybeUser.Value;

            var welcomeEmail = new WelcomeEmail(user.Email, user.FullName);

            await _emailNotificationService.SendWelcomeEmail(welcomeEmail);
        }
    }
}
