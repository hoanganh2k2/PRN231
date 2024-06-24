using BusinessObject.Models;

namespace Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);

    }
}
