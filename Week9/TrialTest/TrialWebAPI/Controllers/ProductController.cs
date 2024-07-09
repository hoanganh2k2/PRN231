using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.IRepository;
using Repositories.Repository;

namespace TrialWebAPI.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _repository;
		public ProductController()
		{
			_repository = new ProductRepository();
		}

		[HttpGet]
		[EnableQuery]
		public async Task<List<Product>> Get()
		{
			List<Product> products = await _repository.GetProductsAsync();
			return products;
		}

		[HttpGet("{id:int}", Name = "GetProduct")]
		public async Task<ActionResult<Product>> GetById(int id)
		{
			if (id <= 0) return BadRequest("Id not Valid");

			Product? existingProduct = await _repository.GetProductAsyncById(id);
			if (existingProduct == null) return NotFound();

			return Ok(existingProduct);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Product product)
		{
			if (product == null) return BadRequest();

			if (!ModelState.IsValid) return BadRequest(ModelState);

			bool result = await _repository.AddProductAsync(product);
			if (result) return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
			return BadRequest("Product creation failed.");
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _repository.Delete(id);
			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Product product)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			try
			{
				await _repository.Update(product);
			}
			catch (Exception)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
