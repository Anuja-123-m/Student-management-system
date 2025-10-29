using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace StudentRecordManagementSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connStr;
        public UserRepository(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("MVCConnectionString")!;
            

        }

        public async Task<AuthUser?> LoginAsync(string username, string password)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_UserLogin", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            await con.OpenAsync();
            using var rdr = await cmd.ExecuteReaderAsync();
            if (await rdr.ReadAsync())
            {
                int? studentId = null;
                try
                {
                    var ord = rdr.GetOrdinal("StudentId");
                    if (!rdr.IsDBNull(ord)) studentId = rdr.GetInt32(ord);
                }
                catch (IndexOutOfRangeException)
                {
                    // Column not present; leave studentId as null
                }

                return new AuthUser(
                    rdr.GetInt32(rdr.GetOrdinal("UserId")),
                    rdr.GetString(rdr.GetOrdinal("Username")),
                    rdr.GetString(rdr.GetOrdinal("RoleName")),
                    studentId
                );
            }
            return null;
        }
    }
}
