using NetCad.Data.Interfaces;
using NetCad.Entity;
using NetCad.Services.Extensions;
using NetCad.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCad.Services
{
    public class StudentNotificationService : IStudentNotificationService
    {
        private readonly IDataContext _dataContext;
        private readonly IMailService _mailService;

        public StudentNotificationService(IDataContext dataContext, IMailService mailService)
        {
            _dataContext = dataContext;
            _mailService = mailService;
        }

        public async Task<List<Student>> GetStudentsRegisteredTodayAsync()
        {
            try
            {
                var students = new List<Student>();
                var query = "SELECT * FROM Students WHERE CAST(RegistrationDateTime AS DATE) = CAST(GETDATE() AS DATE)";
                var command = await _dataContext.CreateCommandAsync(query);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var student = new Student
                        {
                            Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            UniqueId = reader.IsDBNull(1) ? null : reader.GetString(1),
                            FirstName = reader.IsDBNull(2) ? null : reader.GetString(2),
                            LastName = reader.IsDBNull(3) ? null : reader.GetString(3),
                            BirthDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            PlaceOfBirth = reader.IsDBNull(5) ? null : reader.GetString(5),
                            RegistrationDateTime = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)
                        };

                        students.Add(student);
                    }
                }
                return students;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task NotifyRegisteredStudentsAsync()
        {
            try
            {
                var students = await GetStudentsRegisteredTodayAsync();

                if (students != null && students.Count > 0)
                {
                    await _mailService.SendEmailAsync("admin@admin.com", "Registered Students List", students.ToHtmlTable());
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
