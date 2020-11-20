using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EventReminder.Domain.Core.Errors;
using EventReminder.Domain.Core.Primitives;
using EventReminder.Domain.Core.Primitives.Result;

namespace EventReminder.Domain.ValueObjects
{
    /// <summary>
    /// Represents the email value object.
    /// </summary>
    public sealed class Email : ValueObject
    {
        /// <summary>
        /// The email maximum length.
        /// </summary>
        public const int MaxLength = 256;

        private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        private static readonly Lazy<Regex> EmailFormatRegex =
            new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class.
        /// </summary>
        /// <param name="value">The email value.</param>
        private Email(string value) => Value = value;

        /// <summary>
        /// Gets the email value.
        /// </summary>
        public string Value { get; }

        public static implicit operator string(Email email) => email.Value;

        /// <summary>
        /// Creates a new <see cref="Email"/> instance based on the specified value.
        /// </summary>
        /// <param name="email">The email value.</param>
        /// <returns>The result of the email creation process containing the email or an error.</returns>
        public static Result<Email> Create(string email) =>
            Result.Create(email, DomainErrors.Email.NullOrEmpty)
                .Ensure(e => !string.IsNullOrWhiteSpace(e), DomainErrors.Email.NullOrEmpty)
                .Ensure(e => e.Length <= MaxLength, DomainErrors.Email.LongerThanAllowed)
                .Ensure(e => EmailFormatRegex.Value.IsMatch(e), DomainErrors.Email.InvalidFormat)
                .Map(e => new Email(e));

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}