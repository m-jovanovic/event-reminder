using System;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Contracts.Users;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Application.Users.GetUserById
{
    /// <summary>
    /// Represents the query for getting a user by identifier.
    /// </summary>
    public sealed class GetUserByIdQuery : IQuery<Maybe<UserResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdQuery"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public GetUserByIdQuery(Guid userId) => UserId = userId;

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public Guid UserId { get; }
    }
}
