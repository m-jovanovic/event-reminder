using System.Threading.Tasks;
using EventReminder.Contracts.Emails;

namespace EventReminder.Application.Abstractions.Emails
{
    /// <summary>
    /// Represents the email service interface.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends the email with the content based on the specified mail request.
        /// </summary>
        /// <param name="mailRequest">The mail request.</param>
        /// <returns>The completed task.</returns>
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
