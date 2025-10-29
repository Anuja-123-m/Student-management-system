using System.Collections.Generic;
using System.Threading.Tasks;
using StudentRecordManagementSystem.Models;
using StudentRecordManagementSystem.Repository;

namespace StudentRecordManagementSystem.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Task AddStudentAsync(Student student) => _studentRepository.AddStudentAsync(student);
        public Task UpdateMarksAsync(string rollNumber, Student student) => _studentRepository.UpdateMarksAsync(rollNumber, student);
        public Task DisableStudentAsync(string rollNumber) => _studentRepository.DisableStudentAsync(rollNumber);
        public Task<IEnumerable<Student>> GetAllAsync() => _studentRepository.GetAllAsync();
        public Task<Student?> GetByRollAsync(string rollNumber) => _studentRepository.GetByRollAsync(rollNumber);
        public Task<Student?> GetByIdAsync(int id) => _studentRepository.GetByIdAsync(id);
    }
}
