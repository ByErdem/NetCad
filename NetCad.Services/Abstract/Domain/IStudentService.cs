using NetCad.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCad.Services.Interfaces.Domain
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<int> UpdateAsync(Student student);
        Task<int> InsertAsync(Student student);
        Task<int> DeleteAsync(int id);
    }
}
