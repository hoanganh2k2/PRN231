using BusinessObject.Data;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class UserService : IUserService
    {
        private readonly UserDAO _dao;
        public UserService()
        {
            _dao = new UserDAO(new MyDBContext());
        }
        public Task<bool> AddUserAsync(User user)
        {
            return _dao.AddUserAsync(user);
        }

        public Task DeleteUserAsync(int userId)
        {
            return (_dao.DeleteUserAsync(userId));
        }

        public Task<IQueryable<User>> GetAllUsersAsync()
        {
            return _dao.GetAllUsersAsync();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _dao.GetUserByEmailAsync(email);
        }

        public Task<User> GetUserByIdAsync(int userId)
        {
            return _dao.GetUserByIdAsync(userId);
        }

        public Task<User> Login(string email, string password)
        {
            return _dao.Login(email, password);
        }

        public Task UpdateUserAsync(User updatedUser)
        {
            return _dao.UpdateUserAsync(updatedUser);
        }
    }
}
