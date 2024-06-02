using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProductManagementAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsControllers : ControllerBase
    {
        private readonly IProductRepository repository = new ProductRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(repository.GetProducts());
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> GetProduct(int id)
        {
            if (id <= 0)
                return BadRequest();

            Product? p1 = repository.GetProducts().FirstOrDefault(x => x.ProductId == id);

            if (p1 == null)
                return NotFound();

            return Ok(p1);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> PostProduct([FromBody] Product p)
        {
            if (p == null)
                return BadRequest();

            if (repository.GetProducts().FirstOrDefault(
                v => v.ProductName.ToLower() == p.ProductName.ToLower()) != null)
            {
                ModelState.AddModelError("CustomerError", "Product already Exists!");
                return BadRequest(ModelState);
            }

            repository.SaveProduct(p);
            return CreatedAtRoute("GetProduct", new { id = p.ProductId }, p);
        }

        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProduct(int id)
        {
            if (id <= 0)
                return BadRequest();

            Product p = repository.GetProductById(id);
            if (p == null)
                return NotFound();

            repository.DeleteProduct(p);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateProduct(int id, Product p)
        {
            if (id <= 0)
                return BadRequest();

            Product p1 = repository.GetProductById(id);
            if (p1 == null)
                return NotFound();

            p.ProductId = id;

            repository.UpdateProduct(p);
            return NoContent();
        }
    }
}
