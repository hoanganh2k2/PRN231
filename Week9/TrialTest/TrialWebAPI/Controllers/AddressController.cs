using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.IRepository;
using Repositories.Repository;

namespace TrialWebAPI.Controllers
{
	[Route("api/address")]
	[ApiController]
	public class AddressController : ControllerBase
	{
		private readonly IAddressRepository _addressRepository;
		public AddressController()
		{
			_addressRepository = new AddressRepository();
		}

		[HttpGet]
		[EnableQuery]
		public async Task<List<Address>> Get()
		{
			List<Address> addresses = await _addressRepository.GetAddressesAsync();
			return addresses;
		}

		[HttpGet("{id:int}", Name = "GetAddress")]
		public async Task<ActionResult<Address>> GetById(int id)
		{
			if (id <= 0) return BadRequest("Id not Valid");

			Address? existingAddress = await _addressRepository.GetAddressAsyncById(id);
			if (existingAddress == null) return NotFound();

			return Ok(existingAddress);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Address address)
		{
			if (address == null) return BadRequest("Address Null");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			bool result = await _addressRepository.AddAddressAsync(address);
			if (result) return CreatedAtRoute("GetAddress", new { id = address.AddressId }, address);
			return BadRequest("Address creation failed.");
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _addressRepository.Delete(id);
			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Address address)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			try
			{
				await _addressRepository.Update(address);
			}
			catch (Exception)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
