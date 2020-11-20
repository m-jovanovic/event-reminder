using EventReminder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventReminder.Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="Friendship"/> entity.
    /// </summary>
    internal sealed class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(friendship => new
            {
                friendship.UserId,
                friendship.FriendId
            });

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(friendship => friendship.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(friendship => friendship.FriendId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(friendship => friendship.CreatedOnUtc).IsRequired();

            builder.Property(friendship => friendship.ModifiedOnUtc);

            builder.Ignore(friendship => friendship.Id);
        }
    }
}
