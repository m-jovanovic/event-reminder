using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using EventReminder.Application.Core.Abstractions.Messaging;
using EventReminder.Infrastructure.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EventReminder.Infrastructure.Messaging
{
    /// <summary>
    /// Represents the integration event publisher.
    /// </summary>
    internal sealed class IntegrationEventPublisher : IIntegrationEventPublisher, IDisposable
    {
        private readonly MessageBrokerSettings _messageBrokerSettings;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationEventPublisher"/> class.
        /// </summary>
        /// <param name="messageBrokerSettingsOptions">The message broker settings options.</param>
        public IntegrationEventPublisher(IOptions<MessageBrokerSettings> messageBrokerSettingsOptions)
        {
            _messageBrokerSettings = messageBrokerSettingsOptions.Value;

            IConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = _messageBrokerSettings.HostName,
                Port = _messageBrokerSettings.Port,
                UserName = _messageBrokerSettings.UserName,
                Password = _messageBrokerSettings.Password
            };

            _connection = connectionFactory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(_messageBrokerSettings.QueueName, false, false, false);
        }

        /// <inheritdoc />
        public void Publish(IIntegrationEvent integrationEvent)
        {
            string payload = JsonSerializer.Serialize(integrationEvent, typeof(IIntegrationEvent), 
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
                    UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
                    WriteIndented = true,
                });

            byte[] body = Encoding.UTF8.GetBytes(payload);

            _channel.BasicPublish(string.Empty, _messageBrokerSettings.QueueName, body: body);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _connection?.Dispose();

            _channel?.Dispose();
        }
    }
}