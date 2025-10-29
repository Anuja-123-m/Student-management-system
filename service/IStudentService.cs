using System.Collections.Generic;
using System.Threading.Tasks;
using StudentRecordManagementSystem.Models;

namespace StudentRecordManagementSystem.Service
{
    public interface IStudentService
    {
        Task AddStudentAsync(Student student);
        Task UpdateMarksAsync(string rollNumber, Student student);
        Task DisableStudentAsync(string rollNumber);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByRollAsync(string rollNumber);
        Task<Student?> GetByIdAsync(int id);
    }
}
