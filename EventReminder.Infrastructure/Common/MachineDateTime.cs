using System;
using EventReminder.Application.Abstractions.Common;

namespace EventReminder.Infrastructure.Common
{
    /// <summary>
    /// Represents the machine date time service.
    /// </summary>
    internal sealed class MachineDateTime : IDateTime
    {
        /// <inheritdoc />
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
