using System;
using System.Threading.Tasks;
using EventReminder.Domain.Core.Primitives.Maybe;

namespace EventReminder.Domain.Users
{
    /// <summary>
    /// Represents the user repository interface.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets the user with the specified identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The maybe instance that may contain the user with the specified identifier.</returns>
        Task<Maybe<User>> GetByIdAsync(Guid userId);

        /// <summary>
        /// Gets the user with the specified email.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <returns>The maybe instance that may contain the user with the specified email.</returns>
        Task<Maybe<User>> GetByEmailAsync(Email email);

        /// <summary>
        /// Checks if the specified email is unique.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>True if the specified email is unique, otherwise false.</returns>
        Task<bool> IsEmailUniqueAsync(Email email);

        /// <summary>
        /// Inserts the specified user to the database.
        /// </summary>
        /// <param name="user">The user to be inserted to the database.</param>
        void Insert(User user);
    }
}
