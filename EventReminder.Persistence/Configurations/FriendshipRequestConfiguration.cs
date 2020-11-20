using EventReminder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventReminder.Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="FriendshipRequest"/> entity.
    /// </summary>
    internal sealed class FriendshipRequestConfiguration : IEntityTypeConfiguration<FriendshipRequest>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<FriendshipRequest> builder)
        {
            builder.HasKey(friendshipRequest => friendshipRequest.Id);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(friendshipRequest => friendshipRequest.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(friendshipRequest => friendshipRequest.FriendId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(friendshipRequest => friendshipRequest.Accepted).HasDefaultValue(false);

            builder.Property(friendshipRequest => friendshipRequest.Rejected).HasDefaultValue(false);

            builder.Property(friendshipRequest => friendshipRequest.CompletedOnUtc);

            builder.Property(friendshipRequest => friendshipRequest.CreatedOnUtc).IsRequired();

            builder.Property(friendshipRequest => friendshipRequest.ModifiedOnUtc);

            builder.Property(friendshipRequest => friendshipRequest.DeletedOnUtc);

            builder.Property(friendshipRequest => friendshipRequest.Deleted).HasDefaultValue(false);

            builder.HasQueryFilter(friendshipRequest => !friendshipRequest.Deleted);
        }
    }
}
