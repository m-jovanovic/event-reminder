using System.Threading;
using System.Threading.Tasks;

namespace EventReminder.BackgroundTasks.Services
{
    /// <summary>
    /// Represents the email notifications consumer interface.
    /// </summary>
    internal interface IEmailNotificationsConsumer
    {
        /// <summary>
        /// Consumes the event notifications for the specified batch size.
        /// </summary>
        /// <param name="batchSize">The batch size.</param>
        /// <param name="allowedNotificationTimeDiscrepancyInMinutes">
        /// The allowed discrepancy in minutes between the current time and the notification time.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The completed task.</returns>
        Task ConsumeAsync(int batchSize, int allowedNotificationTimeDiscrepancyInMinutes, CancellationToken cancellationToken = default);
    }
}
