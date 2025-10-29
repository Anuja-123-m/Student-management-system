using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StudentRecordManagementSystem.Models;

namespace StudentRecordManagementSystem.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connStr;
        public StudentRepository(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("MVCConnectionString")!;
        }

        

        public async Task AddStudentAsync(Student student)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_AddStudent", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Maths", student.Maths);
            cmd.Parameters.AddWithValue("@Physics", student.Physics);
            cmd.Parameters.AddWithValue("@Chemistry", student.Chemistry);
            cmd.Parameters.AddWithValue("@English", student.English);
            cmd.Parameters.AddWithValue("@Programming", student.Programming);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateMarksAsync(string rollNumber, Student student)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_UpdateStudentMarks", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@RollNumber", rollNumber);
            cmd.Parameters.AddWithValue("@Maths", student.Maths);
            cmd.Parameters.AddWithValue("@Physics", student.Physics);
            cmd.Parameters.AddWithValue("@Chemistry", student.Chemistry);
            cmd.Parameters.AddWithValue("@English", student.English);
            cmd.Parameters.AddWithValue("@Programming", student.Programming);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DisableStudentAsync(string rollNumber)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_DisableStudent", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@RollNumber", rollNumber);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var list = new List<Student>();
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_GetAllStudents", con) { CommandType = CommandType.StoredProcedure };
            await con.OpenAsync();
            using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                list.Add(Map(rdr));
            }
            return list;
        }

        public async Task<Student?> GetByRollAsync(string rollNumber)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_GetStudentByRoll", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@RollNumber", rollNumber);
            await con.OpenAsync();
            using var rdr = await cmd.ExecuteReaderAsync();
            if (await rdr.ReadAsync())
            {
                return Map(rdr);
            }
            return null;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(s => s.Id == id);
        }

        private static Student Map(IDataRecord rdr)
        {
            return new Student
            {
                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                RollNumber = rdr.GetString(rdr.GetOrdinal("RollNumber")),
                Name = rdr.GetString(rdr.GetOrdinal("Name")),
                Maths = rdr.GetInt32(rdr.GetOrdinal("Maths")),
                Physics = rdr.GetInt32(rdr.GetOrdinal("Physics")),
                Chemistry = rdr.GetInt32(rdr.GetOrdinal("Chemistry")),
                English = rdr.GetInt32(rdr.GetOrdinal("English")),
                Programming = rdr.GetInt32(rdr.GetOrdinal("Programming"))
            };
        }
    }
}
