using EventReminder.Application.Abstractions.Data;
using EventReminder.Domain.Events;
using EventReminder.Domain.Friendships;
using EventReminder.Domain.Invitations;
using EventReminder.Domain.Notifications;
using EventReminder.Domain.Users;
using EventReminder.Persistence.Infrastructure;
using EventReminder.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventReminder.Persistence
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey)!;

            services.AddSingleton(new ConnectionString(connectionString));

            services.AddDbContext<EventReminderDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<EventReminderDbContext>());

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<EventReminderDbContext>());

            services.AddScoped<IAttendeeRepository, AttendeeRepository>();

            services.AddScoped<IFriendshipRepository, FriendshipRepository>();

            services.AddScoped<IFriendshipRequestRepository, FriendshipRequestRepository>();

            services.AddScoped<IGroupEventRepository, GroupEventRepository>();

            services.AddScoped<IPersonalEventRepository, PersonalEventRepository>();

            services.AddScoped<IInvitationRepository, InvitationRepository>();

            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
