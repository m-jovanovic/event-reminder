using System;
using System.Linq.Expressions;
using EventReminder.Domain.Notifications;

namespace EventReminder.Persistence.Specifications
{
    /// <summary>
    /// Represents the specification for determining the notifications within a particular time-frame.
    /// </summary>
    internal sealed class NotificationWithinTimeFrameSpecification : Specification<Notification>
    {
        private readonly DateTime _startTime;
        private readonly DateTime _endTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationWithinTimeFrameSpecification"/> class.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <param name="timeFrameInMinutes">The time-frame in minutes.</param>
        internal NotificationWithinTimeFrameSpecification(DateTime utcNow, int timeFrameInMinutes)
        {
            _startTime = utcNow.AddMinutes(-timeFrameInMinutes);
            _endTime = utcNow.AddMinutes(timeFrameInMinutes);
        }

        /// <inheritdoc />
        internal override Expression<Func<Notification, bool>> ToExpression() =>
            notification => !notification.Sent &&
                            notification.DateTimeUtc >= _startTime &&
                            notification.DateTimeUtc <= _endTime;
    }
}
