using System;

namespace EventReminder.Domain.Core.Primitives.Result
{
    /// <summary>
    /// Represents the result of some operation, with status information and possibly a value and an error.
    /// </summary>
    /// <typeparam name="TValue">The result value type.</typeparam>
    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{TValueType}"/> class with the specified parameters.
        /// </summary>
        /// <param name="value">The result value.</param>
        /// <param name="isSuccess">The flag indicating if the result is successful.</param>
        /// <param name="error">The error.</param>
        protected internal Result(TValue value, bool isSuccess, Error error)
            : base(isSuccess, error)
            => _value = value;

        public static implicit operator Result<TValue>(TValue value) => Success(value);

        /// <summary>
        /// Gets the result value if the result is successful, otherwise throws an exception.
        /// </summary>
        /// <returns>The result value if the result is successful.</returns>
        /// <exception cref="InvalidOperationException"> when <see cref="Result.IsFailure"/> is true.</exception>
        public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");
    }
}
