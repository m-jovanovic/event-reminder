using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventReminder.Application.Core.Abstractions.Authentication;
using EventReminder.Application.Core.Abstractions.Common;
using EventReminder.Domain.Entities;
using EventReminder.Infrastructure.Authentication.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EventReminder.Infrastructure.Authentication
{
    /// <summary>
    /// Represents the JWT provider.
    /// </summary>
    internal sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtProvider"/> class.
        /// </summary>
        /// <param name="jwtOptions">The JWT options.</param>
        /// <param name="dateTime">The current date and time.</param>
        public JwtProvider(
            IOptions<JwtSettings> jwtOptions,
            IDateTime dateTime)
        {
            _jwtSettings = jwtOptions.Value;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public string Create(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = 
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("name", user.FullName)
            };

            DateTime tokenExpirationTime = _dateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                null,
                tokenExpirationTime,
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
