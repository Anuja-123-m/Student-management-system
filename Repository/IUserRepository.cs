namespace StudentRecordManagementSystem.Repository
{
    public record AuthUser(int UserId, string Username, string RoleName, int? StudentId);

    public interface IUserRepository
    {
        Task<AuthUser?> LoginAsync(string username, string password);
    }
}
