using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using ProductsAPI.DTO;
using Microsoft.AspNetCore.Authorization;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }

        // localhost:5000/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.Where(i => i.IsActive).Select(p => DTOConverter.ProductToDTO(p)).ToListAsync();
            return Ok(products);
        }

        // localhost:5000/api/products/5 => GET
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p = await _context.Products.Where(i => i.ProductId == id).Select(p => DTOConverter.ProductToDTO(p)).FirstOrDefaultAsync();

            if (p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();//denerken id'yi sil

            return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductId }, entity);//HTTP 201
            //return Ok(entity); // HTTP 200 durum kodu ile ürünün kendisini döndürür
        }

        // localhost:5000/api/products/5 => PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product entity)
        {
            if (id != entity.ProductId)
            {
                return BadRequest();
            }

            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();//İstenen işlemin başarılı olduğunu ancak veri döndürmenin gerekli olmadığı durumlarda kullanılır. put ve delete
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();//İstenen işlemin başarılı olduğunu ancak veri döndürmenin gerekli olmadığı durumlarda kullanılır. put ve delete
        }

        
    }
}
