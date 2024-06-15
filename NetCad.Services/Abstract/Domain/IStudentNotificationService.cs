using NetCad.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCad.Services.Interfaces
{
    public interface IStudentNotificationService
    {
        Task<List<Student>> GetStudentsRegisteredTodayAsync();
        Task NotifyRegisteredStudentsAsync();
    }
}