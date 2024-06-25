using System.Text;
using EventReminder.Application.Abstractions.Authentication;
using EventReminder.Application.Abstractions.Common;
using EventReminder.Application.Abstractions.Cryptography;
using EventReminder.Application.Abstractions.Emails;
using EventReminder.Application.Abstractions.Messaging;
using EventReminder.Application.Abstractions.Notifications;
using EventReminder.Domain.Users;
using EventReminder.Infrastructure.Authentication;
using EventReminder.Infrastructure.Authentication.Settings;
using EventReminder.Infrastructure.Common;
using EventReminder.Infrastructure.Cryptography;
using EventReminder.Infrastructure.Emails;
using EventReminder.Infrastructure.Emails.Settings;
using EventReminder.Infrastructure.Messaging;
using EventReminder.Infrastructure.Messaging.Settings;
using EventReminder.Infrastructure.Notifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EventReminder.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]))
                });

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));

            services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

            services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.SettingsKey));

            services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IPasswordHashChecker, PasswordHasher>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<IEmailNotificationService, EmailNotificationService>();

            services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();

            return services;
        }
    }
}
