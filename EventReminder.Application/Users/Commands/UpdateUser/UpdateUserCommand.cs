using System;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Application.Users.Commands.UpdateUser
{
    /// <summary>
    /// Represents the update user command.
    /// </summary>
    public sealed class UpdateUserCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserCommand"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        public UpdateUserCommand(Guid userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        public string LastName { get; }
    }
}
