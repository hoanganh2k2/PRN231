using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productService = new ProductRepository();

        // GET: api/products
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                List<Product> products = _productService.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                Product? product = _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/products
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product object is null");
                }

                _productService.AddProduct(product);

                return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (product == null || id != product.ProductId)
                {
                    return BadRequest("Invalid product object");
                }

                BusinessObject.Models.Product existingProduct = _productService.GetProductById(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                _productService.UpdateProduct(product);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                BusinessObject.Models.Product existingProduct = _productService.GetProductById(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                _productService.DeleteProduct(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
