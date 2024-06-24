using BusinessObject.Data;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class UserDAO
    {
        private readonly MyDBContext _context;

        public UserDAO(MyDBContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Boolean> AddUserAsync(User user)
        {
            User? find = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (find != null)
            {
                return false;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Read
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IQueryable<User>> GetAllUsersAsync()
        {
            return _context.Users;
        }

        // Update
        public async Task UpdateUserAsync(User updatedUser)
        {
            User? existingUser = await _context.Users.FindAsync(updatedUser.UserId);
            if (existingUser != null)
            {
                existingUser.Email = updatedUser.Email;
                existingUser.Password = updatedUser.Password;
                existingUser.FirstName = updatedUser.FirstName;
                existingUser.LastName = updatedUser.LastName;
                existingUser.MiddleName = updatedUser.MiddleName;
                existingUser.Source = updatedUser.Source;
                _context.Update(existingUser);
                await _context.SaveChangesAsync();
            }
        }

        // Delete
        public async Task DeleteUserAsync(int userId)
        {
            User? userToDelete = await _context.Users.FindAsync(userId);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> Login(string email, string password)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user;
        }
    }
}
