using EventReminder.Domain.Events;
using EventReminder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventReminder.Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="Attendee"/> entity.
    /// </summary>
    internal sealed class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Attendee> builder)
        {
            builder.HasKey(attendee => attendee.Id);

            builder.HasOne<Event>()
                .WithMany()
                .HasForeignKey(attendee => attendee.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(attendee => attendee.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(attendee => attendee.Processed).IsRequired();

            builder.Property(attendee => attendee.CreatedOnUtc).IsRequired();

            builder.Property(attendee => attendee.ModifiedOnUtc);

            builder.Property(attendee => attendee.DeletedOnUtc);

            builder.Property(attendee => attendee.Deleted).HasDefaultValue(false);

            builder.HasQueryFilter(attendee => !attendee.Deleted);
        }
    }
}
