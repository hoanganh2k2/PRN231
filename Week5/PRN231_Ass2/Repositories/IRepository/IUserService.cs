using BusinessObject.Models;

namespace DataAccess.IRepository
{
    public interface IUserService
    {
        Task<Boolean> AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<IQueryable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User updatedUser);
        Task DeleteUserAsync(int userId);
        Task<User> Login(string email, string password);
    }
}
