
namespace EventReminder.Infrastructure.Authentication.Settings
{
    /// <summary>
    /// Represents the JWT configuration settings.
    /// </summary>
    public class JwtSettings
    {
        public const string SettingsKey = "Jwt";

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtSettings"/> class.
        /// </summary>
        public JwtSettings()
        {
            Issuer = string.Empty;
            Audience = string.Empty;
            SecurityKey = string.Empty;
        }

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the security key.
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// Gets or sets the token expiration time in minutes.
        /// </summary>
        public int TokenExpirationInMinutes { get; set; }
    }
}
