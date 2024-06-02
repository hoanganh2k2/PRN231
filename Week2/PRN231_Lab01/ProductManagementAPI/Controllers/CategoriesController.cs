using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProductManagementAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IProductRepository repository = new ProductRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return Ok(repository.GetCategories());
        }
    }
}
