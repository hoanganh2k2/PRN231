using BusinessObject.Model;
using DataAccess;

namespace Repositories.impl
{
    public class ProductRepository : IProductRepository
    {
        public void SaveProduct(Product product) => ProductDAO.SaveProduct(product);
        public Product GetProductById(int id) => ProductDAO.FindProductById(id);
        public List<Product> GetProducts() => ProductDAO.GetProducts();
        public List<Product> Search(string keyword) => ProductDAO.Search(keyword);
        public void UpdateProduct(Product product) => ProductDAO.UpdateProduct(product);
        public void DeleteProduct(Product product) => ProductDAO.DeleteProduct(product);
        public List<OrderDetail> GetOrderDetails(int productId) => OrderDetailDAO.FindAllOrderDetailsByProductId(productId);
    }
}
