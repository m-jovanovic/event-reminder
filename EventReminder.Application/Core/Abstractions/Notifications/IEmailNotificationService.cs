using System.Threading.Tasks;
using EventReminder.Contracts.Emails;

namespace EventReminder.Application.Core.Abstractions.Notifications
{
    /// <summary>
    /// Represents the email notification service interface.
    /// </summary>
    public interface IEmailNotificationService
    {
        /// <summary>
        /// Sends the welcome email notification based on the specified request.
        /// </summary>
        /// <param name="welcomeEmail">The welcome email.</param>
        /// <returns>The completed task.</returns>
        Task SendWelcomeEmail(WelcomeEmail welcomeEmail);

        /// <summary>
        /// Sends the attendee created email.
        /// </summary>
        /// <param name="attendeeCreatedEmail">The attendee created email.</param>
        /// <returns>The completed task.</returns>
        Task SendAttendeeCreatedEmail(AttendeeCreatedEmail attendeeCreatedEmail);

        /// <summary>
        /// Sends the friendship request sent email.
        /// </summary>
        /// <param name="friendshipRequestSentEmail">The friendship request sent email.</param>
        /// <returns>The completed task.</returns>
        Task SendFriendshipRequestSentEmail(FriendshipRequestSentEmail friendshipRequestSentEmail);

        /// <summary>
        /// Sends the friendship request accepted email.
        /// </summary>
        /// <param name="friendshipRequestAcceptedEmail">The friendship request accepted email.</param>
        /// <returns>The completed task.</returns>
        Task SendFriendshipRequestAcceptedEmail(FriendshipRequestAcceptedEmail friendshipRequestAcceptedEmail);

        /// <summary>
        /// Sends the group event cancelled email.
        /// </summary>
        /// <param name="groupEventCancelledEmail">The group event cancelled email.</param>
        /// <returns>The completed task.</returns>
        Task SendGroupEventCancelledEmail(GroupEventCancelledEmail groupEventCancelledEmail);

        /// <summary>
        /// Sends the group event name changed email.
        /// </summary>
        /// <param name="groupEventNameChangedEmail">The group event name changed email.</param>
        /// <returns>The completed task.</returns>
        Task SendGroupEventNameChangedEmail(GroupEventNameChangedEmail groupEventNameChangedEmail);

        /// <summary>
        /// Sends the group event date and time changed email.
        /// </summary>
        /// <param name="groupEventDateAndTimeChangedEmail">The group event date and time changed email.</param>
        /// <returns>The completed task.</returns>
        Task SendGroupEventDateAndTimeChangedEmail(GroupEventDateAndTimeChangedEmail groupEventDateAndTimeChangedEmail);

        /// <summary>
        /// Sends the invitation sent email.
        /// </summary>
        /// <param name="invitationSentEmail">The invitation sent email.</param>
        /// <returns>The completed task.</returns>
        Task SendInvitationSentEmail(InvitationSentEmail invitationSentEmail);

        /// <summary>
        /// Sends the invitation accepted email.
        /// </summary>
        /// <param name="invitationAcceptedEmail">The invitation accepted email.</param>
        /// <returns>The completed task.</returns>
        Task SendInvitationAcceptedEmail(InvitationAcceptedEmail invitationAcceptedEmail);

        /// <summary>
        /// Sends the invitation rejected email.
        /// </summary>
        /// <param name="invitationRejectedEmail">The invitation rejected email.</param>
        /// <returns>The completed task.</returns>
        Task SendInvitationRejectedEmail(InvitationRejectedEmail invitationRejectedEmail);

        /// <summary>
        /// Sends the password changed email.
        /// </summary>
        /// <param name="passwordChangedEmail">The password changed email.</param>
        /// <returns>The completed task.</returns>
        Task SendPasswordChangedEmail(PasswordChangedEmail passwordChangedEmail);

        /// <summary>
        /// Sends the notification email.
        /// </summary>
        /// <param name="notificationEmail">The notification email.</param>
        /// <returns>The completed task.</returns>
        Task SendNotificationEmail(NotificationEmail notificationEmail);
    }
}
