using System;

namespace EventReminder.Domain.Core.Primitives.Result
{
    /// <summary>
    /// Represents a result of some operation, with status information and possibly an error.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class with the specified parameters.
        /// </summary>
        /// <param name="isSuccess">The flag indicating if the result is successful.</param>
        /// <param name="error">The error.</param>
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Gets a value indicating whether the result is a success result.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the result is a failure result.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Gets the error.
        /// </summary>
        public Error Error { get; }

        /// <summary>
        /// Returns a success <see cref="Result"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="Result"/> with the success flag set.</returns>
        public static Result Success() => new Result(true, Error.None);

        /// <summary>
        /// Returns a success <see cref="Result{TValue}"/> with the specified value.
        /// </summary>
        /// <typeparam name="TValue">The result type.</typeparam>
        /// <param name="value">The result value.</param>
        /// <returns>A new instance of <see cref="Result{TValue}"/> with the success flag set.</returns>
        public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, Error.None);

        /// <summary>
        /// Creates a new <see cref="Result{TValue}"/> with the specified nullable value and the specified error.
        /// </summary>
        /// <typeparam name="TValue">The result type.</typeparam>
        /// <param name="value">The result value.</param>
        /// <param name="error">The error in case the value is null.</param>
        /// <returns>A new instance of <see cref="Result{TValue}"/> with the specified value or an error.</returns>
        public static Result<TValue> Create<TValue>(TValue value, Error error)
            where TValue : class
            => value is null ? Failure<TValue>(error) : Success(value);

        /// <summary>
        /// Returns a failure <see cref="Result"/> with the specified error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>A new instance of <see cref="Result"/> with the specified error and failure flag set.</returns>
        public static Result Failure(Error error) => new Result(false, error);

        /// <summary>
        /// Returns a failure <see cref="Result{TValue}"/> with the specified error.
        /// </summary>
        /// <typeparam name="TValue">The result type.</typeparam>
        /// <param name="error">The error.</param>
        /// <returns>A new instance of <see cref="Result{TValue}"/> with the specified error and failure flag set.</returns>
        /// <remarks>
        /// We're purposefully ignoring the nullable assignment here because the API will never allow it to be accessed.
        /// The value is accessed through a method that will throw an exception if the result is a failure result.
        /// </remarks>
        public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default!, false, error);

        /// <summary>
        /// Returns the first failure from the specified <paramref name="results"/>.
        /// If there is no failure, a success is returned.
        /// </summary>
        /// <param name="results">The results array.</param>
        /// <returns>
        /// The first failure from the specified <paramref name="results"/> array,or a success it does not exist.
        /// </returns>
        public static Result FirstFailureOrSuccess(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    return result;
                }
            }

            return Success();
        }
    }
}
