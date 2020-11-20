using System;

namespace EventReminder.Domain.Core.Primitives.Maybe
{
    /// <summary>
    /// Represents a wrapper around a value that may or may not be null.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    public sealed class Maybe<T> : IEquatable<Maybe<T>>
    {
        private readonly T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private Maybe(T value) => _value = value;

        /// <summary>
        /// Gets a value indicating whether or not the value exists.
        /// </summary>
        public bool HasValue => !HasNoValue;

        /// <summary>
        /// Gets a value indicating whether or not the value does not exist.
        /// </summary>
        public bool HasNoValue => _value is null;

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value => HasValue
            ? _value
            : throw new InvalidOperationException("The value can not be accessed because it does not exist.");

        /// <summary>
        /// Gets the default empty instance.
        /// </summary>
        public static Maybe<T> None => new Maybe<T>(default);

        /// <summary>
        /// Creates a new <see cref="Maybe{T}"/> instance based on the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The new <see cref="Maybe{T}"/> instance.</returns>
        public static Maybe<T> From(T value) => new Maybe<T>(value);

        public static implicit operator Maybe<T>(T value) => From(value);

        public static implicit operator T(Maybe<T> maybe) => maybe.Value;

        /// <inheritdoc />
        public bool Equals(Maybe<T> other)
        {
            if (other is null)
            {
                return false;
            }

            if (HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if (HasNoValue || other.HasNoValue)
            {
                return false;
            }

            return Value.Equals(other.Value);
        }

        /// <inheritdoc />
        public override bool Equals(object obj) =>
            obj switch
            {
                null => false,
                T value => Equals(new Maybe<T>(value)),
                Maybe<T> maybe => Equals(maybe),
                _ => false
            };

        /// <inheritdoc />
        public override int GetHashCode() => HasValue ? Value.GetHashCode() : 0;
    }
}
