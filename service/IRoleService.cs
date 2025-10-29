using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRecordManagementSystem.Service
{
    public interface IRoleService
    {
        Task<IEnumerable<string>> GetAllAsync();
        Task<string?> GetByIdAsync(int roleId);
        Task<int?> GetIdByNameAsync(string roleName);
    }
}
