using EventReminder.Domain.Events;
using EventReminder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventReminder.Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="Event"/> entity.
    /// </summary>
    internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(@event => @event.Id);

            builder.HasDiscriminator(@event => @event.EventType)
                .HasValue<PersonalEvent>(EventType.PersonalEvent)
                .HasValue<GroupEvent>(EventType.GroupEvent);

            builder.HasOne<User>().WithMany().HasForeignKey(@event => @event.UserId).IsRequired();

            builder.OwnsOne(@event => @event.Name, nameBuilder =>
            {
                nameBuilder.WithOwner();

                nameBuilder.Property(name => name.Value)
                    .HasColumnName(nameof(Event.Name))
                    .HasMaxLength(Name.MaxLength)
                    .IsRequired();
            });

            builder.OwnsOne(@event => @event.Category, categoryBuilder =>
            {
                categoryBuilder.WithOwner();

                categoryBuilder.Property(category => category.Value)
                    .HasColumnName(nameof(Event.Category))
                    .IsRequired();

                categoryBuilder.Ignore(category => category.Name);
            });

            builder.Property(@event => @event.DateTimeUtc).IsRequired();

            builder.Property(@event => @event.Cancelled).HasDefaultValue(false);

            builder.Property(@event => @event.CreatedOnUtc).IsRequired();

            builder.Property(@event => @event.ModifiedOnUtc);

            builder.Property(@event => @event.DeletedOnUtc);

            builder.Property(@event => @event.Deleted).HasDefaultValue(false);

            builder.HasQueryFilter(@event => !@event.Deleted);
        }
    }
}
