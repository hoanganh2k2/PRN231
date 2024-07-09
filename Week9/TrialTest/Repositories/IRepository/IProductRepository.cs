using BusinessObject.Models;

namespace Repositories.IRepository
{
	public interface IProductRepository
	{
		Task<Boolean> AddProductAsync(Product product);
		Task<Product> GetProductAsyncById(int id);
		Task<Product> GetProductAsyncByName(string name);
		Task<List<Product>> GetProductsAsync();
		Task Update(Product product);
		Task Delete(int productId);
	}
}
