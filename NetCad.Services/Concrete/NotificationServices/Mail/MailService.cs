using NetCad.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace NetCad.Services.Notification.Mail
{
    public class MailService : IMailService
    {
        public MailService(){}

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
