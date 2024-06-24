using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStoreWebAPI.Controllers
{
    public class UserController : ODataController
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [EnableQuery]
        public async Task<IQueryable<User>> Get()
        {
            IQueryable<User> users = await _userService.GetAllUsersAsync();
            return users.AsQueryable();
        }

        // Other CRUD actions without [EnableQuery]

        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _userService.AddUserAsync(user);

            if (result)
            {
                return Created(user);
            }
            else
            {
                return BadRequest("User creation failed.");
            }
        }
        [HttpPut("User")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.UpdateUserAsync(user);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        public async Task<IActionResult> Delete(int key)
        {
            await _userService.DeleteUserAsync(key);

            return NoContent();
        }
    }
}
