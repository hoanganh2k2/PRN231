using BusinessObject.Data;
using BusinessObject.Models;

namespace DataAccess
{
    public class UserDAO
    {
        private readonly MyDBContext _context;
        public UserDAO(MyDBContext context)
        {
            _context = context;
        }

        //Create
        public async Task<Boolean> AddUserAsync(User user)
        {
            //User? find_user = _context.Users.Find()
            _context.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        //Read
        public async Task<User> GetUserAsyncById(int id)
        {
            User user = _context.Users.Where(u => u.UserId == id)
                            .Select(u => new User
                            {
                                UserId = u.UserId,
                                Name = u.Name,
                                AddressId = u.AddressId,
                                Address = _context.Addresses.FirstOrDefault(d => d.AddressId == u.AddressId)
                            }).FirstOrDefault();
            return user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            List<User> users = _context.Users
                .Select(u => new User
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    AddressId = u.AddressId,
                    Address = _context.Addresses.FirstOrDefault(d => d.AddressId == u.AddressId)
                })
                .ToList();

            return users;

        }

        //Update
        public async Task UpdateAsync(User user)
        {
            User? existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.AddressId = user.AddressId;
                _context.Update(existingUser);
                await _context.SaveChangesAsync();
            }
        }

        //Delete
        public async Task DeleteAsync(int userId)
        {
            User? existingUser = await _context.Users.FindAsync(userId);
            if (existingUser != null)
            {
                _context.Users.Remove(existingUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}
