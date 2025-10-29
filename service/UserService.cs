using System.Threading.Tasks;
using StudentRecordManagementSystem.Repository;

namespace StudentRecordManagementSystem.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<AuthUser?> LoginAsync(string username, string password)
        {
            return _userRepository.LoginAsync(username, password);
        }
    }
}
