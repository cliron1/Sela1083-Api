using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop.Infrastructure;
using WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// MyContext is scoped
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
);

var origins = builder.Configuration
    .GetSection("Cors")
    .Get<string[]>();

builder.Services.AddCors(x => x.AddDefaultPolicy(
    opts => opts
        .WithOrigins(origins)
        //.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
  )
);





var app = builder.Build();

// Note: Run "update-database" on application load
await using(var scope = app.Services.CreateAsyncScope()) {
	using var context = scope.ServiceProvider.GetService<MyContext>();
	await context.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
// dotnet ef migration add ""