using BusinessObject.Data;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class AddressDAO
	{
		private readonly MyDBContext _context;
		public AddressDAO(MyDBContext context)
		{
			_context = context;
		}

		//Create
		public async Task<Boolean> AddAddressAsync(Address address)
		{
			Address? existingAddress = _context.Addresses.Find(address.AddressId);
			if (existingAddress != null) return false;

			_context.Addresses.Add(address);
			await _context.SaveChangesAsync();
			return true;
		}

		//Read
		public async Task<Address> GetAddressAsyncById(int id)
		{
			Address? existingAddress = await _context.Addresses.FindAsync(id);

			if (existingAddress != null)
			{
				return existingAddress;
			}
			return null;
		}
		public async Task<Address> GetAddressAsyncByName(string addressName)
		{
			Address? existingAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.AddressName == addressName);

			if (existingAddress != null)
			{
				return existingAddress;
			}
			return null;
		}

		public async Task<List<Address>> GetAddressesAsync() => _context.Addresses.ToList();

		//Update
		public async Task Update(Address address)
		{
			Address? existingAddress = _context.Addresses.Find(address.AddressId);

			if (existingAddress != null)
			{
				existingAddress.AddressName = address.AddressName;
				_context.Update(existingAddress);
				await _context.SaveChangesAsync();
			}

		}

		//Dalete
		public async Task Delete(int addressId)
		{
			Address? existingAddress = _context.Addresses.Find(addressId);

			if (existingAddress != null)
			{
				_context.Remove(existingAddress);
				await _context.SaveChangesAsync();
			}
		}
	}
}

