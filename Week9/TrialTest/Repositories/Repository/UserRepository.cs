using BusinessObject.Data;
using BusinessObject.Models;
using DataAccess;
using Repositories.IRepository;

namespace Repositories.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly UserDAO _dao;
		public UserRepository()
		{
			_dao = new UserDAO(new MyDBContext());
		}
		public Task<bool> AddUserAsync(User user) => _dao.AddUserAsync(user);

		public Task DeleteAsync(int userId) => _dao.DeleteAsync(userId);

		public Task<User> GetUserAsyncById(int id) => _dao.GetUserAsyncById(id);

		public Task<List<User>> GetUsersAsync() => _dao.GetUsersAsync();

		public Task UpdateAsync(User user) => _dao.UpdateAsync(user);
	}
}
