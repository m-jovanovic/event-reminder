using System;
using System.Threading.Tasks;

namespace EventReminder.Domain.Core.Primitives.Maybe
{
    /// <summary>
    /// Contains extension methods for the maybe class.
    /// </summary>
    public static class MaybeExtensions
    {
        /// <summary>
        /// Binds to the result of the function and returns it.
        /// </summary>
        /// <typeparam name="TIn">The result type.</typeparam>
        /// <typeparam name="TOut">The output result type.</typeparam>
        /// <param name="maybe">The result.</param>
        /// <param name="func">The bind function.</param>
        /// <returns>
        /// The success result with the bound value if the current result is a success result, otherwise a failure result.
        /// </returns>
        public static async Task<Maybe<TOut>> Bind<TIn, TOut>(this Maybe<TIn> maybe, Func<TIn, Task<Maybe<TOut>>> func) =>
            maybe.HasValue ? await func(maybe.Value) : Maybe<TOut>.None;

        /// <summary>
        /// Matches to the corresponding functions based on existence of the value.
        /// </summary>
        /// <typeparam name="TIn">The input type.</typeparam>
        /// <typeparam name="TOut">The output type.</typeparam>
        /// <param name="resultTask">The maybe task.</param>
        /// <param name="onSuccess">The on-success function.</param>
        /// <param name="onFailure">The on-failure function.</param>
        /// <returns>
        /// The result of the on-success function if the maybe has a value, otherwise the result of the failure result.
        /// </returns>
        public static async Task<TOut> Match<TIn, TOut>(
            this Task<Maybe<TIn>> resultTask,
            Func<TIn, TOut> onSuccess,
            Func<TOut> onFailure)
        {
            Maybe<TIn> maybe = await resultTask;

            return maybe.HasValue ? onSuccess(maybe.Value) : onFailure();
        }
    }
}
