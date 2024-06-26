using BusinessObject.Model;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            List<Product> products = new();
            try
            {
                using (MyDBContext context = new())
                {
                    products = context.Products.ToList();
                    products.ForEach(f =>
                    {
                        f.Category = context.Categories.Find(f.CategoryID);
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return products;
        }

        public static List<Product> Search(string keyword)
        {
            List<Product> listProducts = new();
            try
            {
                using (MyDBContext context = new())
                {
                    int keywordAsInt;
                    bool isNumeric = Int32.TryParse(keyword, out keywordAsInt);

                    listProducts = context.Products.Where
                        (f => f.ProductName.Contains(keyword) || (isNumeric && f.UnitPrice <= keywordAsInt)).ToList();
                    listProducts.ForEach(f =>
                    {
                        f.Category = context.Categories.Find(f.CategoryID);
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProducts;
        }

        public static List<Product> FindAllProductsByCategoryId(int categoryId)
        {
            List<Product> products = new();
            try
            {
                using (MyDBContext context = new())
                {
                    products = context.Products.Where(f => f.CategoryID == categoryId).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return products;
        }

        public static Product FindProductById(int productId)
        {
            Product product = new();
            try
            {
                using (MyDBContext context = new())
                {
                    product = context.Products.SingleOrDefault(f => f.ProductID == productId);
                    product.Category = context.Categories.Find(product.CategoryID);
                    product.OrderDetails = context.OrderDetails.Where(o => o.ProductID == productId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public static void SaveProduct(Product product)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateProduct(Product product)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    context.Entry(product).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteProduct(Product product)
        {
            try
            {
                using (MyDBContext context = new())
                {
                    Product? productToDelete = context
                        .Products
                        .SingleOrDefault(f => f.ProductID == product.ProductID);
                    context.Products.Remove(productToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
