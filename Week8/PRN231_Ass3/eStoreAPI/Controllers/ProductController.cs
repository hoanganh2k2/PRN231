using BusinessObject.Model;
using eStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository = new ProductRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();

        [HttpGet("Search/{keyword}")]
        public ActionResult<IEnumerable<Product>> Search(string keyword) => repository.Search(keyword);

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id) => repository.GetProductById(id);

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public IActionResult PostProduct(PostProduct postProduct)
        {
            if (repository.GetProducts().FirstOrDefault(f => f.ProductName.ToLower().Equals(postProduct.ProductName.ToLower())) != null)
            {
                return BadRequest();
            }
            Product f = new()
            {
                ProductName = postProduct.ProductName,
                Weight = postProduct.Weight,
                UnitPrice = postProduct.UnitPrice,
                UnitsInStock = postProduct.UnitsInStock,
                CategoryID = postProduct.CategoryID
            };
            repository.SaveProduct(f);
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Product f = repository.GetProductById(id);
            if (f == null)
            {
                return NotFound();
            }
            if (f.OrderDetails != null && f.OrderDetails.Count > 0)
            {
                repository.UpdateProduct(f);
            }
            else
            {
                repository.DeleteProduct(f);
            }
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, PostProduct postProduct)
        {
            Product fTmp = repository.GetProductById(id);
            if (fTmp == null)
            {
                return NotFound();
            }

            if (!fTmp.ProductName.ToLower().Equals(postProduct.ProductName.ToLower())
                && repository.GetProducts().FirstOrDefault(f => f.ProductName.ToLower().Equals(postProduct.ProductName.ToLower())) != null)
            {
                return BadRequest();
            }
            else
            {
                fTmp.ProductName = postProduct.ProductName;
            }

            fTmp.Weight = postProduct.Weight;
            fTmp.UnitPrice = postProduct.UnitPrice;
            fTmp.UnitsInStock = postProduct.UnitsInStock;
            fTmp.CategoryID = postProduct.CategoryID;

            repository.UpdateProduct(fTmp);
            return NoContent();
        }
    }
}
