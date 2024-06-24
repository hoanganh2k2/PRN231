using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            List<Product> listProducts = new();
            try
            {
                using (MyDbContext context = new())
                {
                    listProducts = context.Product.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }
        public static Product FindProductById(int id)
        {
            Product p = new();
            try
            {
                using (MyDbContext context = new())
                {
                    p = context.Product.SingleOrDefault(x => x.ProductId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }
        public static void SaveProduct(Product p)
        {
            try
            {
                using (MyDbContext context = new())
                {
                    context.Product.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateProduct(Product p)
        {
            try
            {
                using (MyDbContext context = new())
                {
                    context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteProduct(Product p)
        {
            try
            {
                using (MyDbContext context = new())
                {
                    Product? p1 = context.Product.SingleOrDefault(c => c.ProductId == p.ProductId);
                    context.Product.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            using (MyDbContext context = new())
            {
                return await context.Category.ToListAsync();
            }
        }
    }
}
