using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryService = new CategoryRepository();

        // GET: api/category
        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                List<BusinessObject.Models.Category> categories = _categoryService.GetCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/category/{id}
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                BusinessObject.Models.Category category = _categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/category
        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Category object is null");
                }

                _categoryService.AddCategory(category);

                return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                if (category == null || id != category.CategoryId)
                {
                    return BadRequest("Invalid category object");
                }

                BusinessObject.Models.Category existingCategory = _categoryService.GetCategoryById(id);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                _categoryService.UpdateCategory(category);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                BusinessObject.Models.Category existingCategory = _categoryService.GetCategoryById(id);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                _categoryService.DeleteCategory(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
