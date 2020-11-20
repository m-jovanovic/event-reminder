namespace EventReminder.Domain.Services
{
    /// <summary>
    /// Represents the password hash checker interfaces.
    /// </summary>
    public interface IPasswordHashChecker
    {
        /// <summary>
        /// Checks if the specified password hash and the provided password hash match.
        /// </summary>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="providedPassword">The provided password.</param>
        /// <returns>True if the password hashes match, otherwise false.</returns>
        bool HashesMatch(string passwordHash, string providedPassword);
    }
}