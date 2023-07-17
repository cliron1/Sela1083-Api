using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Entities;

namespace WebApi.Controllers {
	[ApiController]
	[Route("prods")]
	public class ProductsController : ControllerBase {
		private readonly MyContext _context;

		public ProductsController(MyContext context) {
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
			=> await _context.Products.ToListAsync();

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id) {
			var product = await _context.Products.FindAsync(id);

			if(product == null) {
				return NotFound();
			}

			return product;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id, Product product) {
			if(id != product.Id) {
				return BadRequest();
			}

			_context.Entry(product).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch(DbUpdateConcurrencyException) {
				if(!ProductExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(Product product) {
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProduct", new { id = product.Id }, product);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id) {
			var product = await _context.Products.FindAsync(id);
			if(product == null) {
				return NotFound();
			}

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProductExists(int id) => _context.Products.Any(e => e.Id == id);
	}
}
