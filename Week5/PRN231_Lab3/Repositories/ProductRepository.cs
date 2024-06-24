using BusinessObject.Models;
using DataAccess;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetProducts()
        {
            return ProductDAO.GetProducts();
        }

        public Product GetProductById(int productId)
        {
            return ProductDAO.FindProductById(productId);
        }

        public void AddProduct(Product product)
        {
            try
            {
                ProductDAO.SaveProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                ProductDAO.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                Product product = GetProductById(productId);
                if (product != null)
                {
                    ProductDAO.DeleteProduct(product);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
