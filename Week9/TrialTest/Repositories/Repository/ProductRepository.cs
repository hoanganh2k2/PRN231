using BusinessObject.Data;
using BusinessObject.Models;
using DataAccess;
using Repositories.IRepository;

namespace Repositories.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly ProductDAO _dao;
		public ProductRepository()
		{
			_dao = new ProductDAO(new MyDBContext());
		}

		public Task<bool> AddProductAsync(Product product) => _dao.AddProductAsync(product);

		public Task Delete(int productId) => _dao.Delete(productId);

		public Task<Product> GetProductAsyncById(int id) => _dao.GetProductAsyncById(id);

		public Task<Product> GetProductAsyncByName(string name) => _dao.GetProductAsyncByName(name);

		public Task<List<Product>> GetProductsAsync() => _dao.GetProductsAsync();

		public Task Update(Product product) => _dao.Update(product);
	}
}
