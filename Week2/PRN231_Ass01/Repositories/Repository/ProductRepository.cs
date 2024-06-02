using BusinessObject;
using BusinessObject.Models;
using DataAccess;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class ProductRepository : IProductRepository
    {

        private ProductDAO _context { get; }

        public ProductRepository()
        {
            _context = new ProductDAO(new MyDbContext());
        }
        public async Task AddProductAsync(Product product) => await _context.AddProductAsync(product);

        public async Task DeleteProductAsync(int productId) => await _context.DeleteProductAsync(productId);

        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _context.GetAllProductsAsync();

        public async Task<Product> GetProductByIdAsync(int productId) => await _context.GetProductByIdAsync(productId);

        public async Task<IEnumerable<Product>> SearchProductsAsync(string keyword) => await _context.SearchProductsAsync(keyword);

        public async Task UpdateProductAsync(Product product) => await _context.UpdateProductAsync(product);
        public async Task<IEnumerable<Category>> GetAllCategory() => await _context.GetAllCategory();
    }
}
