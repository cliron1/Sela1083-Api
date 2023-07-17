using Microsoft.EntityFrameworkCore;
using WebApi.Data.Entities;

namespace WebApi.Data;

public class MyContext : DbContext {
	public MyContext(DbContextOptions<MyContext> options)
		: base(options) {
	}

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
}
