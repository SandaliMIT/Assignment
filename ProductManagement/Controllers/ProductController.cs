using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;
using ProductManagement.Services;
using System;
using System.IO;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _productDbContext;
        private readonly IWebHostEnvironment _environment;

        public ProductController(ProductDbContext productDbContext, IWebHostEnvironment environment)
        {
            _productDbContext = productDbContext;
            _environment = environment;
        }

        [HttpGet]
        [Route("Getproduct")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productDbContext.Products.ToListAsync();
        }

        [HttpPost]
        [Route("AddProduct")]
       /* public async Task<IActionResult> AddProduct(IFormFile file, [FromForm] string productName, [FromForm] string description, [FromForm] string createdDate, [FromForm] string category, [FromForm] string price)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No File provided");

            var uniqueFileName = $"{Guid.NewGuid().ToString()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(_environment.ContentRootPath, "images", uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var product = new Product
            {
                ProductName = productName,
                Description = description,
                CreatedDate = createdDate,
                Category = category,
                Price = price,
                PhotoFileName = uniqueFileName
            };

            _productDbContext.Products.Add(product);
            await _productDbContext.SaveChangesAsync();

            return Ok(product);

        }*/

        public async Task<Product> AddProduct(Product objProduct)
        {
            _productDbContext.Products.Add(objProduct);
            await _productDbContext.SaveChangesAsync();
            return objProduct;
        }

        [HttpGet]
        [Route("image/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var imagePath = Path.Combine(_environment.ContentRootPath, "images", fileName);
            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/png");
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
