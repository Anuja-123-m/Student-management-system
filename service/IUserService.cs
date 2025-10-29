using System.Threading.Tasks;
using StudentRecordManagementSystem.Repository;

namespace StudentRecordManagementSystem.Service
{
    public interface IUserService
    {
        Task<AuthUser?> LoginAsync(string username, string password);
    }
}
