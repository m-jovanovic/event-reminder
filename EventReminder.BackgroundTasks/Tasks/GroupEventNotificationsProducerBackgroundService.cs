using System;
using System.Threading;
using System.Threading.Tasks;
using EventReminder.BackgroundTasks.Services;
using EventReminder.BackgroundTasks.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventReminder.BackgroundTasks.Tasks
{
    /// <summary>
    /// Represents the background service for producing group event notifications.
    /// </summary>
    internal sealed class GroupEventNotificationsProducerBackgroundService : BackgroundService
    {
        private readonly ILogger<GroupEventNotificationsProducerBackgroundService> _logger;
        private readonly BackgroundTaskSettings _backgroundTaskSettings;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupEventNotificationsProducerBackgroundService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="backgroundTaskSettingsOptions">The background task settings options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public GroupEventNotificationsProducerBackgroundService(
            ILogger<GroupEventNotificationsProducerBackgroundService> logger,
            IOptions<BackgroundTaskSettings> backgroundTaskSettingsOptions,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _backgroundTaskSettings = backgroundTaskSettingsOptions.Value;
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("GroupEventNotificationsProducerBackgroundService is starting.");

            stoppingToken.Register(() => _logger.LogDebug("GroupEventNotificationsProducerBackgroundService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("GroupEventNotificationsProducerBackgroundService background task is doing background work.");

                await ProduceGroupEventNotificationsAsync(stoppingToken);

                await Task.Delay(_backgroundTaskSettings.SleepTimeInMilliseconds, stoppingToken);
            }

            _logger.LogDebug("GroupEventNotificationsProducerBackgroundService background task is stopping.");

            await Task.CompletedTask;
        }

        /// <summary>
        /// Produces the next batch of group event notifications.
        /// </summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns>The completed task.</returns>
        private async Task ProduceGroupEventNotificationsAsync(CancellationToken stoppingToken)
        {
            try
            {
                using IServiceScope scope = _serviceProvider.CreateScope();

                var groupEventNotificationsProducer = scope.ServiceProvider.GetRequiredService<IGroupEventNotificationsProducer>();

                await groupEventNotificationsProducer.ProduceAsync(_backgroundTaskSettings.AttendeesBatchSize, stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"ERROR: Failed to process the batch of events: {e.Message}", e.Message);
            }
        }
    }
}
