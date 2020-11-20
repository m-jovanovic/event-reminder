using EventReminder.Domain.Core.Primitives;

namespace EventReminder.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static class DomainErrors
    {
        /// <summary>
        /// Contains the user errors.
        /// </summary>
        public static class User
        {
            public static Error NotFound => new Error("User.NotFound", "The user with the specified identifier was not found.");

            public static Error InvalidPermissions => new Error(
                "User.InvalidPermissions",
                "The current user does not have the permissions to perform that operation.");
            
            public static Error DuplicateEmail => new Error("User.DuplicateEmail", "The specified email is already in use.");

            public static Error CannotChangePassword => new Error(
                "User.CannotChangePassword",
                "The password cannot be changed to the specified password.");
        }

        /// <summary>
        /// Contains the attendee errors.
        /// </summary>
        public static class Attendee
        {
            public static Error NotFound => new Error("Attendee.NotFound", "The attendee with the specified identifier was not found.");

            public static Error AlreadyProcessed => new Error("Attendee.AlreadyProcessed", "The attendee has already been processed.");
        }

        /// <summary>
        /// Contains the category errors.
        /// </summary>
        public static class Category
        {
            public static Error NotFound => new Error("Category.NotFound", "The category with the specified identifier was not found.");
        }

        /// <summary>
        /// Contains the event errors.
        /// </summary>
        public static class Event
        {
            public static Error AlreadyCancelled => new Error("Event.AlreadyCancelled", "The event has already been cancelled.");

            public static Error EventHasPassed => new Error(
                "Event.EventHasPassed",
                "The event has already passed and cannot be modified.");
        }

        /// <summary>
        /// Contains the group event errors.
        /// </summary>
        public static class GroupEvent
        {
            public static Error NotFound => new Error(
                "GroupEvent.NotFound",
                "The group event with the specified identifier was not found.");

            public static Error UserNotFound => new Error(
                "GroupEvent.UserNotFound",
                "The user with the specified identifier was not found.");

            public static Error FriendNotFound => new Error(
                "GroupEvent.FriendNotFound",
                "The friend with the specified identifier was not found.");

            public static Error InvitationAlreadySent => new Error(
                "GroupEvent.InvitationAlreadySent",
                "The invitation for this event has already been sent to this user.");

            public static Error NotFriends => new Error(
                "GroupEvent.NotFriends",
                "The specified users are not friend.");

            public static Error DateAndTimeIsInThePast => new Error(
                "GroupEvent.InThePast",
                "The event date and time cannot be in the past.");
        }

        /// <summary>
        /// Contains the personal event errors.
        /// </summary>
        public static class PersonalEvent
        {
            public static Error NotFound => new Error(
                "GroupEvent.NotFound",
                "The group event with the specified identifier was not found.");

            public static Error UserNotFound => new Error(
                "GroupEvent.UserNotFound",
                "The user with the specified identifier was not found.");
            
            public static Error DateAndTimeIsInThePast => new Error(
                "GroupEvent.InThePast",
                "The event date and time cannot be in the past.");

            public static Error AlreadyProcessed => new Error("PersonalEvent.AlreadyProcessed", "The event has already been processed.");
        }

        /// <summary>
        /// Contains the notification errors.
        /// </summary>
        public static class Notification
        {
            public static Error AlreadySent => new Error("Notification.AlreadySent", "The notification has already been sent.");
        }

        /// <summary>
        /// Contains the invitation errors.
        /// </summary>
        public static class Invitation
        {
            public static Error NotFound => new Error(
                "Invitation.NotFound",
                "The invitation with the specified identifier was not found.");

            public static Error EventNotFound => new Error(
                "Invitation.EventNotFound",
                "The event with the specified identifier was not found.");

            public static Error UserNotFound => new Error(
                "Invitation.UserNotFound",
                "The user with the specified identifier was not found.");

            public static Error FriendNotFound => new Error(
                "Invitation.FriendNotFound",
                "The friend with the specified identifier was not found.");

            public static Error AlreadyAccepted => new Error("Invitation.AlreadyAccepted", "The invitation has already been accepted.");

            public static Error AlreadyRejected => new Error("Invitation.AlreadyRejected", "The invitation has already been rejected.");
        }

        /// <summary>
        /// Contains the friendship errors.
        /// </summary>
        public static class Friendship
        {
            public static Error UserNotFound => new Error(
                "Friendship.UserNotFound",
                "The user with the specified identifier was not found.");

            public static Error FriendNotFound => new Error(
                "Friendship.FriendNotFound",
                "The friend with the specified identifier was not found.");

            public static Error NotFriends => new Error(
                "Friendship.NotFriends",
                "The specified users are not friend.");
        }

        /// <summary>
        /// Contains the friendship request errors.
        /// </summary>
        public static class FriendshipRequest
        {
            public static Error NotFound => new Error(
                "FriendshipRequest.NotFound",
                "The friendship request with the specified identifier was not found.");

            public static Error UserNotFound => new Error(
                "FriendshipRequest.UserNotFound",
                "The user with the specified identifier was not found.");

            public static Error FriendNotFound => new Error(
                "FriendshipRequest.FriendNotFound",
                "The friend with the specified identifier was not found.");

            public static Error AlreadyAccepted => new Error(
                "FriendshipRequest.AlreadyAccepted",
                "The friendship request has already been accepted.");

            public static Error AlreadyRejected => new Error(
                "FriendshipRequest.AlreadyRejected",
                "The friendship request has already been rejected.");

            public static Error AlreadyFriends => new Error(
                "FriendshipRequest.AlreadyFriends",
                "The friendship request can not be sent because the users are already friends.");

            public static Error PendingFriendshipRequest => new Error(
                "FriendshipRequest.PendingFriendshipRequest",
                "The friendship request can not be sent because there is a pending friendship request.");
        }

        /// <summary>
        /// Contains the name errors.
        /// </summary>
        public static class Name
        {
            public static Error NullOrEmpty => new Error("Name.NullOrEmpty", "The name is required.");

            public static Error LongerThanAllowed => new Error("Name.LongerThanAllowed", "The name is longer than allowed.");
        }

        /// <summary>
        /// Contains the first name errors.
        /// </summary>
        public static class FirstName
        {
            public static Error NullOrEmpty => new Error("FirstName.NullOrEmpty", "The first name is required.");

            public static Error LongerThanAllowed => new Error("FirstName.LongerThanAllowed", "The first name is longer than allowed.");
        }

        /// <summary>
        /// Contains the last name errors.
        /// </summary>
        public static class LastName
        {
            public static Error NullOrEmpty => new Error("LastName.NullOrEmpty", "The last name is required.");

            public static Error LongerThanAllowed => new Error("LastName.LongerThanAllowed", "The last name is longer than allowed.");
        }

        /// <summary>
        /// Contains the email errors.
        /// </summary>
        public static class Email
        {
            public static Error NullOrEmpty => new Error("Email.NullOrEmpty", "The email is required.");

            public static Error LongerThanAllowed => new Error("Email.LongerThanAllowed", "The email is longer than allowed.");

            public static Error InvalidFormat => new Error("Email.InvalidFormat", "The email format is invalid.");
        }

        /// <summary>
        /// Contains the password errors.
        /// </summary>
        public static class Password
        {
            public static Error NullOrEmpty => new Error("Password.NullOrEmpty", "The password is required.");

            public static Error TooShort => new Error("Password.TooShort", "The password is too short.");

            public static Error MissingUppercaseLetter => new Error(
                "Password.MissingUppercaseLetter",
                "The password requires at least one uppercase letter.");

            public static Error MissingLowercaseLetter => new Error(
                "Password.MissingLowercaseLetter",
                "The password requires at least one lowercase letter.");

            public static Error MissingDigit => new Error(
                "Password.MissingDigit",
                "The password requires at least one digit.");

            public static Error MissingNonAlphaNumeric => new Error(
                "Password.MissingNonAlphaNumeric",
                "The password requires at least one non-alphanumeric.");
        }

        /// <summary>
        /// Contains general errors.
        /// </summary>
        public static class General
        {
            public static Error UnProcessableRequest => new Error(
                "General.UnProcessableRequest",
                "The server could not process the request.");

            public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
        }

        /// <summary>
        /// Contains the authentication errors.
        /// </summary>
        public static class Authentication
        {
            public static Error InvalidEmailOrPassword => new Error(
                "Authentication.InvalidEmailOrPassword",
                "The specified email or password are incorrect.");
        }
    }
}
