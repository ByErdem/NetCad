using NetCad.Data.Interfaces;
using NetCad.Entity;
using NetCad.Services.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCad.Services
{
    public class StudentService : IStudentService
    {
        private readonly IDataContext _dataContext;

        public StudentService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            var students = new List<Student>();
            var query = "SELECT * FROM Students";
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

        public async Task<int> InsertAsync(Student student)
        {
            var query = "INSERT INTO Students (UniqueId, FirstName, LastName, BirthDate, PlaceOfBirth, RegistrationDateTime) VALUES (@UniqueId, @FirstName, @LastName, @BirthDate, @PlaceOfBirth, @RegistrationDateTime)";
            var command = await _dataContext.CreateCommandAsync(query);

            command.Parameters.AddWithValue("@UniqueId", student.UniqueId ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@FirstName", student.FirstName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@LastName", student.LastName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@BirthDate", (object)DBNull.Value);
            command.Parameters.AddWithValue("@PlaceOfBirth", (object)DBNull.Value);
            command.Parameters.AddWithValue("@RegistrationDateTime", DateTime.Now);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> UpdateAsync(Student student)
        {
            var query = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate, PlaceOfBirth = @PlaceOfBirth WHERE Id = @Id";
            var command = await _dataContext.CreateCommandAsync(query);

            command.Parameters.AddWithValue("@FirstName", student.FirstName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@LastName", student.LastName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@BirthDate", student.BirthDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@PlaceOfBirth", student.PlaceOfBirth ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Id", student.Id);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "DELETE FROM Students WHERE Id = @Id";
            var command = await _dataContext.CreateCommandAsync(query);

            command.Parameters.AddWithValue("@Id", id);
            return await command.ExecuteNonQueryAsync();
        }
    }
}
