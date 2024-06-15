using System.Configuration;

namespace NetCad.MailWinService.Models
{
    public class JobSchedule
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string Period { get; set; }

        public static JobSchedule FromConfiguration()
        {
            return new JobSchedule
            {
                Hour = int.Parse(ConfigurationManager.AppSettings["JobSchedule:Hour"]),
                Minute = int.Parse(ConfigurationManager.AppSettings["JobSchedule:Minute"]),
                Period = ConfigurationManager.AppSettings["JobSchedule:Period"]
            };
        }
    }
}
