using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton<IPeopleService, PeopleService>();

builder.Services.AddCors(x => x.AddDefaultPolicy(
    opts => opts.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
  )
);





var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
