using EventReminder.Domain.Entities;
using EventReminder.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventReminder.Persistence.Configurations
{
    /// <summary>
    /// Represents the configuration for the <see cref="User"/> entity.
    /// </summary>
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.OwnsOne(user => user.FirstName, firstNameBuilder =>
            {
                firstNameBuilder.WithOwner();

                firstNameBuilder.Property(firstName => firstName.Value)
                    .HasColumnName(nameof(User.FirstName))
                    .HasMaxLength(FirstName.MaxLength)
                    .IsRequired();
            });

            builder.OwnsOne(user => user.LastName, lastNameBuilder =>
            {
                lastNameBuilder.WithOwner();

                lastNameBuilder.Property(lastName => lastName.Value)
                    .HasColumnName(nameof(User.LastName))
                    .HasMaxLength(LastName.MaxLength)
                    .IsRequired();
            });

            builder.OwnsOne(user => user.Email, emailBuilder =>
            {
                emailBuilder.WithOwner();

                emailBuilder.Property(email => email.Value)
                    .HasColumnName(nameof(User.Email))
                    .HasMaxLength(Email.MaxLength)
                    .IsRequired();
            });

            builder.Property<string>("_passwordHash")
                .HasField("_passwordHash")
                .HasColumnName("PasswordHash")
                .IsRequired();

            builder.Property(user => user.CreatedOnUtc).IsRequired();

            builder.Property(user => user.ModifiedOnUtc);

            builder.Property(user => user.DeletedOnUtc);

            builder.Property(user => user.Deleted).HasDefaultValue(false);

            builder.HasQueryFilter(user => !user.Deleted);

            builder.Ignore(user => user.FullName);
        }
    }
}
