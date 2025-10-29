using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace StudentRecordManagementSystem.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _connStr;
        public RoleRepository(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("MVCConnectionString")!;
        }

        public async Task<IEnumerable<string>> GetAllAsync()
        {
            var roles = new List<string>();
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("SELECT RoleName FROM Role ORDER BY RoleName", con);
            await con.OpenAsync();
            using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                roles.Add(rdr.GetString(0));
            }
            return roles;
        }

        public async Task<string?> GetByIdAsync(int roleId)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("SELECT RoleName FROM Role WHERE RoleId = @RoleId", con);
            cmd.Parameters.AddWithValue("@RoleId", roleId);
            await con.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return result as string;
        }

        public async Task<int?> GetIdByNameAsync(string roleName)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("SELECT RoleId FROM Role WHERE RoleName = @RoleName", con);
            cmd.Parameters.AddWithValue("@RoleName", roleName);
            await con.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return result == null ? null : (int?)Convert.ToInt32(result);
        }
    }
}
