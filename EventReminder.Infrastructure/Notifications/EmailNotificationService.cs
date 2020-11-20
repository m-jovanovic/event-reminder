using System;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Emails;
using EventReminder.Application.Core.Abstractions.Notifications;
using EventReminder.Contracts.Emails;

namespace EventReminder.Infrastructure.Notifications
{
    /// <summary>
    /// Represents the email notification service.
    /// </summary>
    internal sealed class EmailNotificationService : IEmailNotificationService
    {
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationService"/> class.
        /// </summary>
        /// <param name="emailService">The email service.</param>
        public EmailNotificationService(IEmailService emailService) => _emailService = emailService;

        /// <inheritdoc />
        public async Task SendWelcomeEmail(WelcomeEmail welcomeEmail)
        {
            var mailRequest = new MailRequest(
                welcomeEmail.EmailTo,
                "Welcome to Event Reminder! 🎉",
                $"Welcome to Event Reminder {welcomeEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"You have registered with the email {welcomeEmail.EmailTo}.");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendAttendeeCreatedEmail(AttendeeCreatedEmail attendeeCreatedEmail)
        {
            var mailRequest = new MailRequest(
                attendeeCreatedEmail.EmailTo,
                $"Attending {attendeeCreatedEmail.EventName} 🎊",
                $"Hello {attendeeCreatedEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"Your invitation has been processed and you are now attending {attendeeCreatedEmail.EventName} ({attendeeCreatedEmail.EventDateAndTime}).");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendFriendshipRequestSentEmail(FriendshipRequestSentEmail friendshipRequestSentEmail)
        {
            var mailRequest = new MailRequest(
                friendshipRequestSentEmail.EmailTo,
                $"Friendship request from {friendshipRequestSentEmail.UserWhoSentRequest} 👋",
                $"Hello {friendshipRequestSentEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"The user {friendshipRequestSentEmail.UserWhoSentRequest} has sent you a friendship request.");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendFriendshipRequestAcceptedEmail(FriendshipRequestAcceptedEmail friendshipRequestAcceptedEmail)
        {
            var mailRequest = new MailRequest(
                friendshipRequestAcceptedEmail.EmailTo,
                "Friendship request accepted 😁",
                $"Hello {friendshipRequestAcceptedEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"The user {friendshipRequestAcceptedEmail.FriendName} has accepted your friendship request.");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendGroupEventCancelledEmail(GroupEventCancelledEmail groupEventCancelledEmail)
        {
            var mailRequest = new MailRequest(
                groupEventCancelledEmail.EmailTo,
                $"{groupEventCancelledEmail.EventName} has been cancelled 😞",
                $"Hello {groupEventCancelledEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"Unfortunately, the event {groupEventCancelledEmail.EventName} ({groupEventCancelledEmail.EventDateAndTime}) has been cancelled.");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendGroupEventNameChangedEmail(GroupEventNameChangedEmail groupEventNameChangedEmail)
        {
            var mailRequest = new MailRequest(
                groupEventNameChangedEmail.EmailTo,
                $"{groupEventNameChangedEmail.EventName} has been renamed! 👌",
                $"Hello {groupEventNameChangedEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"The event {groupEventNameChangedEmail.OldEventName} has been renamed to" +
                $"{groupEventNameChangedEmail.EventName} ({groupEventNameChangedEmail.EventDateAndTime}).");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendGroupEventDateAndTimeChangedEmail(GroupEventDateAndTimeChangedEmail groupEventDateAndTimeChangedEmail)
        {
            var mailRequest = new MailRequest(
                groupEventDateAndTimeChangedEmail.EmailTo,
                $"{groupEventDateAndTimeChangedEmail.EventName} has been moved! 👌",
                $"Hello {groupEventDateAndTimeChangedEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"The event {groupEventDateAndTimeChangedEmail.EventName} originally" +
                $"scheduled on {groupEventDateAndTimeChangedEmail.OldEventDateAndTime} has" +
                $"been moved to {groupEventDateAndTimeChangedEmail.EventDateAndTime}.");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendInvitationSentEmail(InvitationSentEmail invitationSentEmail)
        {
            var mailRequest = new MailRequest(
                invitationSentEmail.EmailTo,
                $"You have an invitation to {invitationSentEmail.EventName}! 🎉",
                $"Hello {invitationSentEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"You have a new invitation to the event {invitationSentEmail.EventName} ({invitationSentEmail.EventDateAndTime}).");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendInvitationAcceptedEmail(InvitationAcceptedEmail invitationAcceptedEmail)
        {
            var mailRequest = new MailRequest(
                invitationAcceptedEmail.EmailTo,
                "Invitation accepted 😁",
                $"Hello {invitationAcceptedEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"Your friend {invitationAcceptedEmail.FriendName} has accepted your invitation to {invitationAcceptedEmail.EventName} ({invitationAcceptedEmail.EventDateAndTime}).");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendInvitationRejectedEmail(InvitationRejectedEmail invitationRejectedEmail)
        {
            var mailRequest = new MailRequest(
                invitationRejectedEmail.EmailTo,
                "Invitation rejected 😞",
                $"Hello {invitationRejectedEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"Your friend {invitationRejectedEmail.FriendName} has rejected your invitation to {invitationRejectedEmail.EventName} ({invitationRejectedEmail.EventDateAndTime}).");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendPasswordChangedEmail(PasswordChangedEmail passwordChangedEmail)
        {
            var mailRequest = new MailRequest(
                passwordChangedEmail.EmailTo,
                "Password changed 🔐",
                $"Hello {passwordChangedEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                "Your password was successfully changed.");

            await _emailService.SendEmailAsync(mailRequest);
        }

        /// <inheritdoc />
        public async Task SendNotificationEmail(NotificationEmail notificationEmail)
        {
            var mailRequest = new MailRequest(notificationEmail.EmailTo, notificationEmail.Subject, notificationEmail.Body);

            await _emailService.SendEmailAsync(mailRequest);
        }
    }
}
