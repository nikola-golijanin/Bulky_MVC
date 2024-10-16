using Microsoft.AspNetCore.Identity.UI.Services;

namespace Service;
public class EmailSenderService : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage) =>
        //logic to send email
        Task.CompletedTask;
}
