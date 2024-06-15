using NetCad.Data;
using NetCad.Data.Interfaces;
using NetCad.MailWinService.Models;
using NetCad.Services;
using NetCad.Services.Interfaces;
using NetCad.Services.Notification.Mail;
using System.Configuration;
using System.ServiceProcess;

namespace NetCad.MailWinService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            IMailService mailService = new MailService();
            IDataContext dataContext = new SqlDataContext(connectionString);
            IStudentNotificationService studentService = new StudentNotificationService(dataContext, mailService);
            JobSchedule jobSchedule = JobSchedule.FromConfiguration();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
            new Service1(studentService, jobSchedule)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
