using NetCad.MailWinService.Models;
using NetCad.Services.Interfaces;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace NetCad.MailWinService
{
    public partial class Service1 : ServiceBase
    {
        private readonly IStudentNotificationService _studentService;
        private readonly JobSchedule _jobSchedule;
        private Timer _timer;

        public Service1(IStudentNotificationService studentService, JobSchedule jobSchedule)
        {
            InitializeComponent();
            _studentService = studentService;
            _jobSchedule = jobSchedule;
        }

        protected override void OnStart(string[] args)
        {
            ScheduleJob();
        }

        private void ScheduleJob()
        {
            var scheduledHour = _jobSchedule.Hour;
            var scheduledMinute = _jobSchedule.Minute;
            var period = _jobSchedule.Period;

            if (period.Equals("PM", StringComparison.OrdinalIgnoreCase) && scheduledHour < 12)
            {
                scheduledHour += 12;
            }
            else if (period.Equals("AM", StringComparison.OrdinalIgnoreCase) && scheduledHour == 12)
            {
                scheduledHour = 0;
            }

            var now = DateTime.Now;
            var scheduledTime = DateTime.Today.AddHours(scheduledHour).AddMinutes(scheduledMinute);

            if (now > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }

            var timeToGo = scheduledTime - now;

            if (timeToGo <= TimeSpan.Zero)
            {
                timeToGo = TimeSpan.Zero;
            }

            _timer = new Timer(async _ =>
            {
                await DoWorkAsync();
                ScheduleJob(); // Ertesi gün aynı saatte tekrar zamanla
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        private async Task DoWorkAsync()
        {
            try
            {
                await _studentService.NotifyRegisteredStudentsAsync();
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnStop()
        {
            _timer?.Dispose();
        }
    }
}
