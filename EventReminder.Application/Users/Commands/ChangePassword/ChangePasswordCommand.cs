using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.Users.Commands.ChangePassword
{
    /// <summary>
    /// Represents the change password command.
    /// </summary>
    public sealed class ChangePasswordCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordCommand"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The new password.</param>
        public ChangePasswordCommand(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the new password.
        /// </summary>
        public string Password { get; }
    }
}
