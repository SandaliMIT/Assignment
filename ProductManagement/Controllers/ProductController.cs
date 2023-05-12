using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _productDbContext;

        public ProductController(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        [HttpGet]
        [Route("Getproduct")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productDbContext.Products.ToListAsync();
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<Product> AddProduct(Product objProduct)
        {
            _productDbContext.Products.Add(objProduct);
            await _productDbContext.SaveChangesAsync();
            return objProduct;
        }

        [HttpPatch]
        [Route("UpdateProduct/{id}")]
        public async Task<Product> UpdateProduct(Product objProduct)
        {
            _productDbContext.Entry(objProduct).State = EntityState.Modified;
            await _productDbContext.SaveChangesAsync();
            return objProduct;
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public bool DeleteProduct(int id)
        {
            bool status = false;
            var product = _productDbContext.Products.Find(id);
            if (product != null)
            {
                status = true;
                _productDbContext.Entry(product).State = EntityState.Deleted;
                _productDbContext.SaveChanges();
            }
            else
            {
                status = false;
            }

            return status;
        }
    }
}
