using BusinessObject.Models;

namespace Repositories.IRepository
{
	public interface IUserRepository
	{
		Task<Boolean> AddUserAsync(User user);
		Task<User> GetUserAsyncById(int id);
		Task<List<User>> GetUsersAsync();
		Task UpdateAsync(User user);
		Task DeleteAsync(int userId);

	}
}
