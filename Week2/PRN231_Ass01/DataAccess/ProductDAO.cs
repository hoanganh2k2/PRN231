using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductDAO
    {
        private readonly MyDbContext _dbContext;

        public ProductDAO(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task AddProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            Product? product = await _dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTxt)
        {
            List<Product> products;

            if (decimal.TryParse(searchTxt, out decimal searchTxtPrice))
            {
                products = await _dbContext.Products
                    .Where(p => p.ProductName.Contains(searchTxt) || p.UnitPrice <= searchTxtPrice)
                    .Include(p => p.Category)
                    .ToListAsync();
            }
            else
            {
                products = await _dbContext.Products
                    .Where(p => p.ProductName.Contains(searchTxt))
                    .Include(p => p.Category)
                    .ToListAsync();
            }

            return products;
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _dbContext.Categories.ToListAsync();
        }
    }
}
