using BusinessObject.Models;

namespace Repositories.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task<IEnumerable<Product>> SearchProductsAsync(string keyword);
        Task<IEnumerable<Category>> GetAllCategory();
    }
}
