using StudentRecordManagementSystem.Models;

namespace StudentRecordManagementSystem.Repository
{
    public interface IStudentRepository
    {
        Task AddStudentAsync(Student student);
        Task UpdateMarksAsync(string rollNumber, Student student);
        Task DisableStudentAsync(string rollNumber);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByRollAsync(string rollNumber);
        Task<Student?> GetByIdAsync(int id);
        
    }
}
