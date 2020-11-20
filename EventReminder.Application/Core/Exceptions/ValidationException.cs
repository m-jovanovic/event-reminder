using System;
using System.Collections.Generic;
using System.Linq;
using EventReminder.Domain.Core.Primitives;
using FluentValidation.Results;

namespace EventReminder.Application.Core.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a validation fails.
    /// </summary>
    public sealed class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="failures">The collection of validation failures.</param>
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("One or more validation failures has occurred.") =>
            Errors = failures
                .Distinct()
                .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
                .ToList();

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        public IReadOnlyCollection<Error> Errors { get; }
    }
}
