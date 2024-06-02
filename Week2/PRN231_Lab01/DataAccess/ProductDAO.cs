using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            List<Product> listProduct = new();
            try
            {
                using (MyDbContext context = new())
                {
                    listProduct = context.Products.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProduct;
        }
        public static Product FindProductById(int id)
        {
            Product p = new();
            try
            {
                using (MyDbContext context = new())
                {
                    p = context.Products.SingleOrDefault(x => x.ProductId == id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }
        public static void SaveProduct(Product p)
        {
            try
            {
                using (MyDbContext context = new())
                {
                    Category? existingCategory = context.Categories.Find(p.CategoryId);
                    if (existingCategory != null)
                    {
                        context.Entry(existingCategory).State = EntityState.Unchanged;
                        p.Category = existingCategory;
                    }

                    context.Products.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateProduct(Product p)
        {
            try
            {
                using (MyDbContext context = new())
                {
                    context.Entry<Product>(p).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteProduct(Product p)
        {
            try
            {
                using (MyDbContext context = new())
                {
                    Product? p1 = context.Products.SingleOrDefault(
                        c => c.ProductId == p.ProductId
                        );
                    context.Products.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
