using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApplicationProject.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Buraya email gönderme işlemlerini yapabilirsin burada
            return Task.CompletedTask;
        }
    }
}
