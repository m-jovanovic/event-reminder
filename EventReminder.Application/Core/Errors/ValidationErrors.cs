using EventReminder.Domain.Core.Primitives;

namespace EventReminder.Application.Core.Errors
{
    /// <summary>
    /// Contains the validation errors.
    /// </summary>
    internal static class ValidationErrors
    {
        /// <summary>
        /// Contains the login errors.
        /// </summary>
        internal static class Login
        {
            internal static Error EmailIsRequired => new Error("Login.EmailIsRequired", "The email is required.");

            internal static Error PasswordIsRequired => new Error("Login.PasswordIsRequired", "The password is required.");
        }

        /// <summary>
        /// Contains the reject friendship request errors.
        /// </summary>
        internal static class RejectFriendshipRequest
        {
            internal static Error FriendshipRequestIdIsRequired => new Error(
                "RejectFriendshipRequest.FriendshipRequestIdIsRequired",
                "The invitation identifier is required.");
        }

        /// <summary>
        /// Contains the accept friendship request errors.
        /// </summary>
        internal static class AcceptFriendshipRequest
        {
            internal static Error FriendshipRequestIdIsRequired => new Error(
                "AcceptFriendshipRequest.FriendshipRequestIdIsRequired",
                "The invitation identifier is required.");
        }

        /// <summary>
        /// Contains the send remove friendship errors.
        /// </summary>
        internal static class RemoveFriendship
        {
            internal static Error UserIdIsRequired => new Error(
                "RemoveFriendship.UserIdIsRequired",
                "The user identifier is required.");

            internal static Error FriendIdIsRequired => new Error(
                "RemoveFriendship.FriendIdIsRequired",
                "The friend identifier is required.");
        }

        /// <summary>
        /// Contains the create group event errors.
        /// </summary>
        internal static class CreateGroupEvent
        {
            internal static Error UserIdIsRequired => new Error("CreateGroupEvent.UserIdIsRequired", "The user identifier is required.");

            internal static Error NameIsRequired => new Error("CreateGroupEvent.NameIsRequired", "The event name is required.");

            internal static Error CategoryIdIsRequired => new Error(
                "CreateGroupEvent.CategoryIdIsRequired",
                "The category identifier is required.");

            internal static Error DateAndTimeIsRequired => new Error(
                "CreateGroupEvent.DateAndTimeIsRequired",
                "The date and time of the event is required.");
        }

        /// <summary>
        /// Contains the update group event errors.
        /// </summary>
        internal static class UpdateGroupEvent
        {
            internal static Error GroupEventIdIsRequired => new Error(
                "UpdateGroupEvent.GroupEventIdIsRequired",
                "The group event identifier is required.");
            
            internal static Error NameIsRequired => new Error("UpdateGroupEvent.NameIsRequired", "The event name is required.");

            internal static Error DateAndTimeIsRequired => new Error(
                "UpdateGroupEvent.DateAndTimeIsRequired",
                "The date and time of the event is required.");
        }

        /// <summary>
        /// Contains the cancel group event errors.
        /// </summary>
        internal static class CancelGroupEvent
        {
            internal static Error GroupEventIdIsRequired => new Error(
                "CancelGroupEvent.GroupEventIdIsRequired",
                "The group event identifier is required.");
        }

        /// <summary>
        /// Contains the invite friend to group event errors.
        /// </summary>
        internal static class InviteFriendToGroupEvent
        {
            internal static Error GroupEventIdIsRequired => new Error(
                "InviteFriendToGroupEvent.GroupEventIdIsRequired",
                "The group event identifier is required.");

            internal static Error FriendIdIsRequired => new Error(
                "InviteFriendToGroupEvent.FriendIdIsRequired",
                "The friend identifier is required.");
        }

        /// <summary>
        /// Contains the accept invitation errors.
        /// </summary>
        internal static class AcceptInvitation
        {
            internal static Error InvitationIdIsRequired => new Error(
                "AcceptInvitation.InvitationIdIsRequired",
                "The invitation identifier is required.");
        }

        /// <summary>
        /// Contains the reject invitation errors.
        /// </summary>
        internal static class RejectInvitation
        {
            internal static Error InvitationIdIsRequired => new Error(
                "RejectInvitation.InvitationIdIsRequired",
                "The invitation identifier is required.");
        }

        /// <summary>
        /// Contains the create personal event errors.
        /// </summary>
        internal static class CreatePersonalEvent
        {
            internal static Error UserIdIsRequired => new Error(
                "CreatePersonalEvent.UserIdIsRequired",
                "The user identifier is required.");

            internal static Error NameIsRequired => new Error("CreatePersonalEvent.NameIsRequired", "The event name is required.");

            internal static Error CategoryIdIsRequired => new Error(
                "CreatePersonalEvent.CategoryIdIsRequired",
                "The category identifier is required.");

            internal static Error DateAndTimeIsRequired => new Error(
                "CreatePersonalEvent.DateAndTimeIsRequired",
                "The date and time of the event is required.");
        }

        /// <summary>
        /// Contains the update personal event errors.
        /// </summary>
        internal static class UpdatePersonalEvent
        {
            internal static Error GroupEventIdIsRequired => new Error(
                "UpdatePersonalEvent.GroupEventIdIsRequired",
                "The group event identifier is required.");

            internal static Error NameIsRequired => new Error("UpdatePersonalEvent.NameIsRequired", "The event name is required.");

            internal static Error DateAndTimeIsRequired => new Error(
                "UpdatePersonalEvent.DateAndTimeIsRequired",
                "The date and time of the event is required.");
        }

        /// <summary>
        /// Contains the cancel personal event errors.
        /// </summary>
        internal static class CancelPersonalEvent
        {
            internal static Error PersonalEventIdIsRequired => new Error(
                "CancelPersonalEvent.GroupEventIdIsRequired",
                "The group event identifier is required.");
        }

        /// <summary>
        /// Contains the change password errors.
        /// </summary>
        internal static class ChangePassword
        {
            internal static Error UserIdIsRequired => new Error("ChangePassword.UserIdIsRequired", "The user identifier is required.");

            internal static Error PasswordIsRequired => new Error("ChangePassword.PasswordIsRequired", "The password is required.");
        }

        /// <summary>
        /// Contains the create user errors.
        /// </summary>
        internal static class CreateUser
        {
            internal static Error FirstNameIsRequired => new Error("CreateUser.FirstNameIsRequired", "The first name is required.");

            internal static Error LastNameIsRequired => new Error("CreateUser.LastNameIsRequired", "The last name is required.");

            internal static Error EmailIsRequired => new Error("CreateUser.EmailIsRequired", "The email is required.");

            internal static Error PasswordIsRequired => new Error("CreateUser.PasswordIsRequired", "The password is required.");
        }
        
        /// <summary>
        /// Contains the send friendship request errors.
        /// </summary>
        internal static class SendFriendshipRequest
        {
            internal static Error UserIdIsRequired => new Error(
                "SendFriendshipRequest.UserIdIsRequired",
                "The user identifier is required.");

            internal static Error FriendIdIsRequired => new Error(
                "SendFriendshipRequest.FriendIdIsRequired",
                "The friend identifier is required.");
        }

        /// <summary>
        /// Contains the update user errors.
        /// </summary>
        internal static class UpdateUser
        {
            internal static Error UserIdIsRequired => new Error("UpdateUser.UserIdIsRequired", "The user identifier is required.");

            internal static Error FirstNameIsRequired => new Error("UpdateUser.FirstNameIsRequired", "The first name is required.");

            internal static Error LastNameIsRequired => new Error("UpdateUser.LastNameIsRequired", "The last name is required.");
        }
    }
}
