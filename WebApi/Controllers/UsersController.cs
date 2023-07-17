using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Entities;

namespace WebApi.Controllers {
	[ApiController]
	[Route("users")]
	public class UsersController : ControllerBase {
		private readonly MyContext _context;

		public UsersController(MyContext context) {
			_context = context;
		}

		/// <summary>Get all users from the database</summary>
		// GET: api/Users
		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
			=> await _context.Users.ToListAsync();

		/// <summary>Get a single User by hid/her ID</summary>
		// GET: api/Users/5
		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(int id) {
			var user = await _context.Users.FindAsync(id);

			if(user == null) {
				return NotFound();
			}

			return user;
		}

		// PUT: api/Users/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutUser(int id, User user) {
			if(id != user.Id) {
				return BadRequest();
			}

			_context.Entry(user).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch(DbUpdateConcurrencyException) {
				if(!UserExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Users
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<User>> PostUser(User user) {
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetUser", new { id = user.Id }, user);
		}

		// DELETE: api/Users/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id) {
			var user = await _context.Users.FindAsync(id);
			if(user == null) {
				return NotFound();
			}

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool UserExists(int id) => _context.Users.Any(e => e.Id == id);
	}
}
