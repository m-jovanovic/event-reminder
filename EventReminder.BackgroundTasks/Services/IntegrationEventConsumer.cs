using EventReminder.Application.Abstractions.Messaging;
using MediatR;

namespace EventReminder.BackgroundTasks.Services
{
    /// <summary>
    /// Represents the integration event consumer.
    /// </summary>
    internal sealed class IntegrationEventConsumer : IIntegrationEventConsumer
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationEventConsumer"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public IntegrationEventConsumer(IMediator mediator) => _mediator = mediator;

        /// <inheritdoc />
        public void Consume(IIntegrationEvent integrationEvent) => _mediator.Publish(integrationEvent).GetAwaiter().GetResult();
    }
}