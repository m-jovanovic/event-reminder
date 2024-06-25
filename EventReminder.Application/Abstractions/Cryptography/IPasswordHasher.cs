using EventReminder.Domain.Users;

namespace EventReminder.Application.Abstractions.Cryptography
{
    /// <summary>
    /// Represents the password hasher interface.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes the specified password.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>The password hash.</returns>
        string HashPassword(Password password);
    }
}
