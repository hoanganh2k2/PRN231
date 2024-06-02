using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Repositories.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eStoreAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository = new ProductRepository();

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (id <= 0)
                return BadRequest();

            Product? product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                if (product == null)
                    return NotFound();

            return Ok(product);
        }

        [HttpGet("Category")]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _productRepository.GetAllCategory();
        }


        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {
            await _productRepository.AddProductAsync(product);
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateProduct")]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
            return NoContent();
        }


        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IEnumerable<Product>> Search(string keyword)
        {
            return await _productRepository.SearchProductsAsync(keyword);
        }
    }
}
