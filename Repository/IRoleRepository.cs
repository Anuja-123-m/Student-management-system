namespace StudentRecordManagementSystem.Repository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<string>> GetAllAsync();
        Task<string?> GetByIdAsync(int roleId);
        Task<int?> GetIdByNameAsync(string roleName);
    }
}
