using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.IRepository;
using Repositories.Repository;

namespace TrialWebAPI.Controllers
{
	[Route("api/user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		public UserController()
		{
			_userRepository = new UserRepository();
		}

		[HttpGet]
		[EnableQuery]
		public async Task<List<User>> Get()
		{
			List<User> users = await _userRepository.GetUsersAsync();
			return users;
		}

		[HttpGet("{id:int}", Name = "GetUser")]
		public async Task<ActionResult<User>> GetById(int id)
		{
			if (id <= 0) return BadRequest("Id not Valid");

			User? existingUser = await _userRepository.GetUserAsyncById(id);
			if (existingUser == null) return NotFound();

			return Ok(existingUser);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] User user)
		{
			if (user == null) return BadRequest();

			if (!ModelState.IsValid) return BadRequest(ModelState);

			bool result = await _userRepository.AddUserAsync(user);
			if (result) return CreatedAtRoute("GetUser", new { id = user.UserId }, user);
			return BadRequest("User creation failed.");
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _userRepository.DeleteAsync(id);
			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] User user)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			try
			{
				await _userRepository.UpdateAsync(user);
			}
			catch (Exception)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
