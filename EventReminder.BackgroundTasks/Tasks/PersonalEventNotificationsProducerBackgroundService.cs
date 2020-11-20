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
    internal sealed class PersonalEventNotificationsProducerBackgroundService : BackgroundService
    {
        private readonly ILogger<PersonalEventNotificationsProducerBackgroundService> _logger;
        private readonly BackgroundTaskSettings _backgroundTaskSettings;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEventNotificationsProducerBackgroundService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="backgroundTaskSettingsOptions">The background task settings options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public PersonalEventNotificationsProducerBackgroundService(
            ILogger<PersonalEventNotificationsProducerBackgroundService> logger,
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
            _logger.LogDebug("PersonalEventNotificationsProducerBackgroundService is starting.");

            stoppingToken.Register(() => _logger.LogDebug("PersonalEventNotificationsProducerBackgroundService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("PersonalEventNotificationsProducerBackgroundService background task is doing background work.");

                await ProducePersonalEventNotificationsAsync(stoppingToken);

                await Task.Delay(_backgroundTaskSettings.SleepTimeInMilliseconds, stoppingToken);
            }

            _logger.LogDebug("PersonalEventNotificationsProducerBackgroundService background task is stopping.");

            await Task.CompletedTask;
        }

        /// <summary>
        /// Produces the next batch of group event notifications.
        /// </summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns>The completed task.</returns>
        private async Task ProducePersonalEventNotificationsAsync(CancellationToken stoppingToken)
        {
            try
            {
                using IServiceScope scope = _serviceProvider.CreateScope();

                var personalEventNotificationsProducer = scope.ServiceProvider.GetRequiredService<IPersonalEventNotificationsProducer>();

                await personalEventNotificationsProducer.ProduceAsync(_backgroundTaskSettings.PersonalEventsBatchSize, stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"ERROR: Failed to process the batch of events: {e.Message}", e.Message);
            }
        }
    }
}
