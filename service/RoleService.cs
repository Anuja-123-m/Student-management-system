using System.Collections.Generic;
using System.Threading.Tasks;
using StudentRecordManagementSystem.Repository;

namespace StudentRecordManagementSystem.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<IEnumerable<string>> GetAllAsync() => _roleRepository.GetAllAsync();
        public Task<string?> GetByIdAsync(int roleId) => _roleRepository.GetByIdAsync(roleId);
        public Task<int?> GetIdByNameAsync(string roleName) => _roleRepository.GetIdByNameAsync(roleName);
    }
}
