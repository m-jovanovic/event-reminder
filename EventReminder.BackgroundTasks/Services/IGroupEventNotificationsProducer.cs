using System.Threading;
using System.Threading.Tasks;

namespace EventReminder.BackgroundTasks.Services
{
    /// <summary>
    /// Represents the group events notifications producer interface.
    /// </summary>
    internal interface IGroupEventNotificationsProducer
    {
        /// <summary>
        /// Produces group event notifications for the specified batch size.
        /// </summary>
        /// <param name="batchSize">The batch size.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The completed task.</returns>
        Task ProduceAsync(int batchSize, CancellationToken cancellationToken = default);
    }
}
