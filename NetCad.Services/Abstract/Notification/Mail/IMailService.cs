using System.Threading.Tasks;

namespace NetCad.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
