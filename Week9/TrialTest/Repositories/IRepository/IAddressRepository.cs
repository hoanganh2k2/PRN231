using BusinessObject.Models;

namespace Repositories.IRepository
{
	public interface IAddressRepository
	{
		Task<Boolean> AddAddressAsync(Address address);
		Task<Address> GetAddressAsyncById(int id);
		Task<Address> GetAddressAsyncByName(string addressName);
		Task<List<Address>> GetAddressesAsync();
		Task Update(Address address);
		Task Delete(int addressId);
	}
}
