using EventReminder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventReminder.Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="PersonalEvent"/> entity.
    /// </summary>
    internal sealed class PersonalEventConfiguration : IEntityTypeConfiguration<PersonalEvent>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<PersonalEvent> builder) =>
            builder.Property(personalEvent => personalEvent.Processed)
                .IsRequired()
                .HasDefaultValue(false);
    }
}