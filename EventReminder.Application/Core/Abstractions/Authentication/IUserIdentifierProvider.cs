using System;

namespace EventReminder.Application.Core.Abstractions.Authentication
{
    /// <summary>
    /// Represents the user identifier provider interface.
    /// </summary>
    public interface IUserIdentifierProvider
    {
        /// <summary>
        /// Gets the authenticated user identifier.
        /// </summary>
        Guid UserId { get; }
    }
}
