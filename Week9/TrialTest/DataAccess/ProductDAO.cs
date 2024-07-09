using BusinessObject.Data;
using BusinessObject.Models;

namespace DataAccess
{
    public class ProductDAO
    {
        private readonly MyDBContext _context;
        public ProductDAO(MyDBContext context)
        {
            _context = context;
        }

        //Create
        public async Task<Boolean> AddProductAsync(Product product)
        {
            Product? existingProduct = _context.Products.FirstOrDefault(p => p.ProductName == product.ProductName);
            if (existingProduct != null) return false;

            _context.Add(product);
            await _context.SaveChangesAsync();
            return true;
        }

        //Read
        public async Task<Product> GetProductAsyncById(int id)
        {
            Product existingProduct = _context.Products.Where(p => p.ProductId == id)
                        .Select(p => new Product
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            Description = p.Description,
                            UserId = p.UserId,
                            Price = p.Price,
                            User = _context.Users.FirstOrDefault(u => u.UserId == u.UserId)
                        }).FirstOrDefault();
            if (existingProduct != null) return existingProduct;
            return null;
        }
        public async Task<Product> GetProductAsyncByName(string name)
        {
            Product? existingProduct = _context.Products.FirstOrDefault(p => p.ProductName == name);
            if (existingProduct != null) return existingProduct;
            return null;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = _context.Products
                .Select(p => new Product
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    UserId = p.UserId,
                    Price = p.Price,
                    User = _context.Users.FirstOrDefault(u => u.UserId == p.UserId)
                })
                .ToList();

            return products;
        }

        //Update
        public async Task Update(Product product)
        {
            Product? existingProduct = _context.Products.Find(product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.UserId = product.UserId;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;

                _context.Update(existingProduct);
                await _context.SaveChangesAsync();
            }
        }

        //Delete
        public async Task Delete(int productId)
        {
            Product? existingProduct = _context.Products.Find(productId);
            if (existingProduct != null)
            {
                _context.Remove(existingProduct);
                await _context.SaveChangesAsync();
            }
        }
    }
}
