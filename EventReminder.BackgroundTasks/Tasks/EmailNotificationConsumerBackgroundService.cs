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
    /// Represents the email notification consumer background service.
    /// </summary>
    internal sealed class EmailNotificationConsumerBackgroundService : BackgroundService
    {
        private readonly ILogger<EmailNotificationConsumerBackgroundService> _logger;
        private readonly BackgroundTaskSettings _backgroundTaskSettings;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationConsumerBackgroundService"/>
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="backgroundTaskSettingsOptions">The background settings options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public EmailNotificationConsumerBackgroundService(
            ILogger<EmailNotificationConsumerBackgroundService> logger,
            IOptions<BackgroundTaskSettings> backgroundTaskSettingsOptions,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _backgroundTaskSettings = backgroundTaskSettingsOptions.Value;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("EmailNotificationConsumerBackgroundService is starting.");

            stoppingToken.Register(() => _logger.LogDebug("EmailNotificationConsumerBackgroundService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("EmailNotificationConsumerBackgroundService background task is doing background work.");

                await ConsumeEventNotificationsAsync(stoppingToken);

                await Task.Delay(_backgroundTaskSettings.SleepTimeInMilliseconds, stoppingToken);
            }

            _logger.LogDebug("EmailNotificationConsumerBackgroundService background task is stopping.");

            await Task.CompletedTask;
        }

        /// <summary>
        /// Consumes the next batch of event notifications.
        /// </summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns>The completed task.</returns>
        private async Task ConsumeEventNotificationsAsync(CancellationToken stoppingToken)
        {
            try
            {
                using IServiceScope scope = _serviceProvider.CreateScope();

                var emailNotificationsConsumer = scope.ServiceProvider.GetRequiredService<IEmailNotificationsConsumer>();

                await emailNotificationsConsumer.ConsumeAsync(
                    _backgroundTaskSettings.NotificationsBatchSize,
                    _backgroundTaskSettings.AllowedNotificationTimeDiscrepancyInMinutes,
                    stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"ERROR: Failed to process the batch of notifications: {e.Message}", e.Message);
            }
        }
    }
}
