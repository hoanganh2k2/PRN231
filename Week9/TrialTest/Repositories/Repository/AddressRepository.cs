using BusinessObject.Data;
using BusinessObject.Models;
using DataAccess;
using Repositories.IRepository;

namespace Repositories.Repository
{
	public class AddressRepository : IAddressRepository
	{
		private readonly AddressDAO _dao;
		public AddressRepository()
		{
			_dao = new AddressDAO(new MyDBContext());
		}

		public Task<bool> AddAddressAsync(Address address) => _dao.AddAddressAsync(address);

		public Task Delete(int addressId) => _dao.Delete(addressId);

		public Task<Address> GetAddressAsyncById(int id) => _dao.GetAddressAsyncById(id);

		public Task<Address> GetAddressAsyncByName(string addressName) => _dao.GetAddressAsyncByName(addressName);

		public Task<List<Address>> GetAddressesAsync() => _dao.GetAddressesAsync();

		public Task Update(Address address) => _dao.Update(address);
	}
}
