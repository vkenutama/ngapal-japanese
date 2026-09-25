using Microsoft.EntityFrameworkCore;
using ngapal_jepang_be.Data;
using ngapal_jepang_be.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    DbInitializer.Seed(dbContext);
}

// Map endpoints
app.MapUserEndpoints();
app.MapDeckEndpoint();
app.MapLearnEndpoints();
app.MapFlashcardEnpoint();

app.UseAuthorization();
app.Run();