using BusinessObject.Model;
using eStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace eStoreAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repository = new CategoryRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories() => repository.GetCategories();

        [HttpPost]
        public IActionResult PostCategory(Category category)
        {
            repository.SaveCategory(category);
            return NoContent();
        }
    }
}
