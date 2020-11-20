using System;
using System.Threading.Tasks;

namespace EventReminder.Domain.Core.Primitives.Result
{
    /// <summary>
    /// Contains extension methods for the result class.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Ensures that the specified predicate is true, otherwise returns a failure result with the specified error.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="error">The error.</param>
        /// <returns>
        /// The success result if the predicate is true and the current result is a success result, otherwise a failure result.
        /// </returns>
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return result.IsSuccess && predicate(result.Value) ? result : Result.Failure<T>(error);
        }

        /// <summary>
        /// Maps the result value to a new value based on the specified mapping function.
        /// </summary>
        /// <typeparam name="TIn">The result type.</typeparam>
        /// <typeparam name="TOut">The output result type.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="func">The mapping function.</param>
        /// <returns>
        /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
        /// </returns>
        public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func) =>
            result.IsSuccess ? func(result.Value) : Result.Failure<TOut>(result.Error);

        /// <summary>
        /// Binds to the result of the function and returns it.
        /// </summary>
        /// <typeparam name="TIn">The result type.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="func">The bind function.</param>
        /// <returns>
        /// The success result with the bound value if the current result is a success result, otherwise a failure result.
        /// </returns>
        public static async Task<Result> Bind<TIn>(this Result<TIn> result, Func<TIn, Task<Result>> func) =>
            result.IsSuccess ? await func(result.Value) : Result.Failure(result.Error);

        /// <summary>
        /// Binds to the result of the function and returns it.
        /// </summary>
        /// <typeparam name="TIn">The result type.</typeparam>
        /// <typeparam name="TOut">The output result type.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="func">The bind function.</param>
        /// <returns>
        /// The success result with the bound value if the current result is a success result, otherwise a failure result.
        /// </returns>
        public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func) =>
            result.IsSuccess ? await func(result.Value) : Result.Failure<TOut>(result.Error);

        /// <summary>
        /// Matches the success status of the result to the corresponding functions.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="resultTask">The result task.</param>
        /// <param name="onSuccess">The on-success function.</param>
        /// <param name="onFailure">The on-failure function.</param>
        /// <returns>
        /// The result of the on-success function if the result is a success result, otherwise the result of the failure result.
        /// </returns>
        public static async Task<T> Match<T>(this Task<Result> resultTask, Func<T> onSuccess, Func<Error, T> onFailure)
        {
            Result result = await resultTask;

            return result.IsSuccess ? onSuccess() : onFailure(result.Error);
        }

        /// <summary>
        /// Matches the success status of the result to the corresponding functions.
        /// </summary>
        /// <typeparam name="TIn">The result type.</typeparam>
        /// <typeparam name="TOut">The output result type.</typeparam>
        /// <param name="resultTask">The result task.</param>
        /// <param name="onSuccess">The on-success function.</param>
        /// <param name="onFailure">The on-failure function.</param>
        /// <returns>
        /// The result of the on-success function if the result is a success result, otherwise the result of the failure result.
        /// </returns>
        public static async Task<TOut> Match<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> onSuccess,
            Func<Error, TOut> onFailure)
        {
            Result<TIn> result = await resultTask;

            return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
        }
    }
}
